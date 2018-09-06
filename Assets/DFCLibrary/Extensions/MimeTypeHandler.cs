using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MimeTypeHandler
{
    public enum MimeTypes
    {
        //text
        text_plain,
        text_html,
        text_css,
        text_javascript,

        //images
        img_gif,
        img_jpeg,
        img_bmp,
        img_png,
        img_svg_xml,

        //audio
        audio_midi,
        audio_mpeg,
        audio_webm,
        audio_ogg,
        audio_wav,

        //video
        video_webm,
        video_ogg,

        //application
        application_octect_stream,
        application_pkcs12,
        application_vnd_mspowerpoint,
        application_xhtml_xml,
        application_xml,
        application_pdf
    };

    public static string ToString(this MimeTypes type)
    {
        string name = "";

        switch (type)
        {
            case MimeTypes.application_octect_stream:
                name = "application/octet-stream";
                break;

            case MimeTypes.application_pdf:
                name = "application/pdf";
                break;

            case MimeTypes.application_pkcs12:
                name = "application/pkcs12";
                break;

            case MimeTypes.application_vnd_mspowerpoint:
                name = "application/vnd.mspowerpoint";
                break;

            case MimeTypes.application_xhtml_xml:
                name = "application/xhtml+xml";
                break;

            case MimeTypes.application_xml:
                name = "application/xml";
                break;

            case MimeTypes.audio_midi:
                name = "audio/midi";
                break;

            case MimeTypes.audio_mpeg:
                name = "audio/mpeg";
                break;

            case MimeTypes.audio_ogg:
                name = "audio/ogg";
                break;

            case MimeTypes.audio_wav:
                name = "audio/wav";
                break;

            case MimeTypes.audio_webm:
                name = "audio/webm";
                break;

            case MimeTypes.img_bmp:
                name = "image/bmp";
                break;
            
            case MimeTypes.img_gif:
                name = "image/gif";
                break;

            case MimeTypes.img_jpeg:
                name = "image/jpeg";
                break;

            case MimeTypes.img_png:
                name = "image/png";
                break;

            case MimeTypes.img_svg_xml:
                name = "image/svg+xml";
                break;

            case MimeTypes.text_css:
                name = "text/css";
                break;

            case MimeTypes.text_html:
                name = "text/html";
                break;

            case MimeTypes.text_javascript:
                name = "text/javascript";
                break;

            case MimeTypes.text_plain:
                name = "text/plain";
                break;

            case MimeTypes.video_ogg:
                name = "video/ogg";
                break;

            case MimeTypes.video_webm:
                name = "video/webm";
                break;
        }

        return name;
    }

    public static string GetExtension(this MimeTypes type)
    {
        string ext = "";

        switch (type)
        {
            case MimeTypes.application_octect_stream:
                ext = ".octet-stream";
                break;

            case MimeTypes.application_pdf:
                ext = ".pdf";
                break;

            case MimeTypes.application_pkcs12:
                ext = ".pkcs12";
                break;

            case MimeTypes.application_vnd_mspowerpoint:
                ext = ".vnd.mspowerpoint";
                break;

            case MimeTypes.application_xhtml_xml:
                ext = ".xhtml+xml";
                break;

            case MimeTypes.application_xml:
                ext = ".xml";
                break;

            case MimeTypes.audio_midi:
                ext = ".midi";
                break;

            case MimeTypes.audio_mpeg:
                ext = ".mpeg";
                break;

            case MimeTypes.audio_ogg:
                ext = ".ogg";
                break;

            case MimeTypes.audio_wav:
                ext = ".wav";
                break;

            case MimeTypes.audio_webm:
                ext = ".webm";
                break;

            case MimeTypes.img_bmp:
                ext = ".bmp";
                break;

            case MimeTypes.img_gif:
                ext = ".gif";
                break;

            case MimeTypes.img_jpeg:
                ext = ".jpeg";
                break;

            case MimeTypes.img_png:
                ext = ".png";
                break;

            case MimeTypes.img_svg_xml:
                ext = ".svg+xml";
                break;

            case MimeTypes.text_css:
                ext = ".css";
                break;

            case MimeTypes.text_html:
                ext = ".html";
                break;

            case MimeTypes.text_javascript:
                ext = ".javascript";
                break;

            case MimeTypes.text_plain:
                ext = ".plain";
                break;

            case MimeTypes.video_ogg:
                ext = ".ogg";
                break;

            case MimeTypes.video_webm:
                ext = ".webm";
                break;
        }

        return ext;
    }
}