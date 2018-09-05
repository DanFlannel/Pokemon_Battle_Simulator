using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using ThreadPriority = System.Threading.ThreadPriority;

/// <summary> The ProGif Decoder worker. </summary>
internal sealed class ProGifDeWorker
{
	private static ProGifDeWorker _proGifDeWorker = null;
	/// <summary>
	/// Create the decoder worker as a singleton, queue all gif decoders in one separated thread,
	/// better performance than just multi-threads because more decoder worker threads may cause too many threads switching issue, 
	/// thus has a bad effect on performance.
	/// </summary>
	internal static ProGifDeWorker GetInstance(ThreadPriority priority = ThreadPriority.BelowNormal, bool isBackgroundThread = true)
	{
		if (_proGifDeWorker == null)
		{
			_proGifDeWorker = new ProGifDeWorker(priority, isBackgroundThread);
		}
		return _proGifDeWorker;
	}

	private Thread _thread = null;

    private ThreadPriority _priority;

    private bool _isNewThread = false;

	internal List<ProGifDecoder> m_Decoders = new List<ProGifDecoder>();

	internal ProGifDeWorker(ThreadPriority priority, bool isBackgroundThread)
	{
		_priority = priority;
		_isNewThread = true;
		_Init(isBackgroundThread);
	}

	private void _Init(bool isBackgroundThread)
	{
        if (_isNewThread)
		{
			_thread = new Thread(Run);
			_thread.Priority = _priority;
			_thread.IsBackground = isBackgroundThread;
		}
	}

	internal void QueueDecoder(ProGifDecoder decoder)
	{
        if (!m_Decoders.Contains(decoder))
        {
            m_Decoders.Add(decoder);
        }
	}

    internal void DeQueueDecoder(ProGifDecoder decoder)
    {
        if (m_Decoders.Contains(decoder))
        {
            decoder.ThreadClear();
			int decoderIndex = m_Decoders.IndexOf(decoder);
			if(decoderIndex >= 0 && m_Decoders.Count > decoderIndex) m_Decoders[decoderIndex] = null;
        }
        if (_GetDecoderCount() <= 0) _Abort();
    }

	internal void Start()
	{
		if (_isNewThread)
		{
			_isNewThread = false;
			_thread.Start();
		}
	}

	private void _Abort()
	{
        if (_thread.ThreadState == ThreadState.Aborted) return;
		_running = false;
		m_Decoders.Clear();
        _proGifDeWorker = null;
        _thread.Abort();
    }

    private int _GetDecoderCount()
    {
        int count = 0;
        for (int i = 0; i < m_Decoders.Count; i++)
        {
            if (m_Decoders[i] != null)
            {
                count++;
            }
        }
        return count;
    }

    private bool _running = false;
	private void Run()
	{
		if (_running)
		{
			return;
		}

		_running = true;
        for (int i = 0; i < m_Decoders.Count; i++)
        {
            if (_running && m_Decoders[i] != null)
            {
                if (!m_Decoders[i].runningInThread && !m_Decoders[i].threadAborted)
                {
                    m_Decoders[i].StartDecode(i, (decoderIndex) => {
                        if (decoderIndex >= 0 && m_Decoders.Count > decoderIndex) m_Decoders[decoderIndex] = null;
                    });
                }
            }
        }
		_Abort();
	}
}
