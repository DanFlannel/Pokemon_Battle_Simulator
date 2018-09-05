/// <summary>
/// Created by SwanOB2
/// </summary>

using System.Collections.Generic;

#region ------ Upload Json Object ------
public class GiphyUpload
{
	public class Response
	{
		public Data data;
		public Meta meta;
	}

	public class Data
	{
		public string id;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}
#endregion

#region ------ Normal Json Object ------
public class GiphySearch
{
	public class Response
	{
		public List<Datum> data;
		public Pagination pagination;
		public Meta meta;
	}

	public class FixedHeightStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class OriginalStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedWidth
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmallStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedHeightDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class Preview
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedHeightSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Downsized
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedLarge
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmallStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class PreviewWebp
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedWidthSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedSmall
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedWidthDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedMedium
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Original
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string frames;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeight
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class Looping
	{
		public string mp4;
		public string mp4_size;
	}

	public class OriginalMp4
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class PreviewGif
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Images
	{
		public FixedHeightStill fixed_height_still;
		public OriginalStill original_still;
		public FixedWidth fixed_width;
		public FixedHeightSmallStill fixed_height_small_still;
		public FixedHeightDownsampled fixed_height_downsampled;
		public Preview preview;
		public FixedHeightSmall fixed_height_small;
		public DownsizedStill downsized_still;
		public Downsized downsized;
		public DownsizedLarge downsized_large;
		public FixedWidthSmallStill fixed_width_small_still;
		public PreviewWebp preview_webp;
		public FixedWidthStill fixed_width_still;
		public FixedWidthSmall fixed_width_small;
		public DownsizedSmall downsized_small;
		public FixedWidthDownsampled fixed_width_downsampled;
		public DownsizedMedium downsized_medium;
		public Original original;
		public FixedHeight fixed_height;
		public Looping looping;
		public OriginalMp4 original_mp4;
		public PreviewGif preview_gif;
	}

	public class User
	{
		public string avatar_url;
		public string banner_url;
		public string profile_url;
		public string username;
		public string display_name;
		public string twitter;
	}

	public class Datum
	{
		public string type;
		public string id;
		public string slug;
		public string url;
		public string bitly_gif_url;
		public string bitly_url;
		public string embed_url;
		public string username;
		public string source;
		public string rating;
		public string content_url;
		public string source_tld;
		public string source_post_url;
		public int is_indexable;
		public string import_datetime;
		public string trending_datetime;
		public Images images;
		public User user;
	}

	public class Pagination
	{
		public int total_count;
		public int count;
		public int offset;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}
	
public class GiphyGetById
{
	public class Response
	{
		public Data data;
		public Meta meta;
	}

	public class User
	{
		public string avatar_url;
		public string banner_url;
		public string profile_url;
		public string username;
		public string display_name;
	}

	public class FixedHeight
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedHeightDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class FixedWidth
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedWidthStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmallStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedWidthSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedWidthSmallStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class Downsized
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class DownsizedLarge
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedMedium
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Original
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string frames;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
		public string hash;
	}

	public class OriginalStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Looping
	{
		public string mp4;
	}

	public class Images
	{
		public FixedHeight fixed_height;
		public FixedHeightStill fixed_height_still;
		public FixedHeightDownsampled fixed_height_downsampled;
		public FixedWidth fixed_width;
		public FixedWidthStill fixed_width_still;
		public FixedWidthDownsampled fixed_width_downsampled;
		public FixedHeightSmall fixed_height_small;
		public FixedHeightSmallStill fixed_height_small_still;
		public FixedWidthSmall fixed_width_small;
		public FixedWidthSmallStill fixed_width_small_still;
		public Downsized downsized;
		public DownsizedStill downsized_still;
		public DownsizedLarge downsized_large;
		public DownsizedMedium downsized_medium;
		public Original original;
		public OriginalStill original_still;
		public Looping looping;
	}

	public class Data
	{
		public string type;
		public string id;
		public string slug;
		public string url;
		public string bitly_gif_url;
		public string bitly_url;
		public string embed_url;
		public string username;
		public string source;
		public string rating;
		public string content_url;
		public User user;
		public string source_tld;
		public string source_post_url;
		public int is_indexable;
		public string import_datetime;
		public string trending_datetime;
		public Images images;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}
	
public class GiphyGetByIds
{
	public class Response
	{
		public List<Datum> data;
		public Pagination pagination;
		public Meta meta;
	}

	public class User
	{
		public string avatar_url;
		public string banner_url;
		public string profile_url;
		public string username;
		public string display_name;
	}

	public class FixedHeight
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedHeightDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class FixedWidth
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedWidthStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmallStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedWidthSmallStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Downsized
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedLarge
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedMedium
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Original
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string frames;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
		public string hash;
	}

	public class OriginalStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Looping
	{
		public string mp4;
		public string mp4_size;
	}

	public class OriginalMp4
	{
		public string mp4;
		public string mp4_size;
		public string width;
		public string height;
	}

	public class Preview
	{
		public string mp4;
		public string mp4_size;
		public string width;
		public string height;
	}

	public class DownsizedSmall
	{
		public string mp4;
		public string mp4_size;
		public string width;
		public string height;
	}

	public class PreviewGif
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class PreviewWebp
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Images
	{
		public FixedHeight fixed_height;
		public FixedHeightStill fixed_height_still;
		public FixedHeightDownsampled fixed_height_downsampled;
		public FixedWidth fixed_width;
		public FixedWidthStill fixed_width_still;
		public FixedWidthDownsampled fixed_width_downsampled;
		public FixedHeightSmall fixed_height_small;
		public FixedHeightSmallStill fixed_height_small_still;
		public FixedWidthSmall fixed_width_small;
		public FixedWidthSmallStill fixed_width_small_still;
		public Downsized downsized;
		public DownsizedStill downsized_still;
		public DownsizedLarge downsized_large;
		public DownsizedMedium downsized_medium;
		public Original original;
		public OriginalStill original_still;
		public Looping looping;
		public OriginalMp4 original_mp4;
		public Preview preview;
		public DownsizedSmall downsized_small;
		public PreviewGif preview_gif;
		public PreviewWebp preview_webp;
	}

	public class Datum
	{
		public string type;
		public string id;
		public string slug;
		public string url;
		public string bitly_gif_url;
		public string bitly_url;
		public string embed_url;
		public string username;
		public string source;
		public string rating;
		public string content_url;
		public User user;
		public string source_tld;
		public string source_post_url;
		public int is_indexable;
		public string import_datetime;
		public string trending_datetime;
		public Images images;
	}

	public class Pagination
	{
		public int total_count;
		public int count;
		public int offset;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}
	
public class GiphyRandom
{
	public class Response
	{
		public Data data;
		public Meta meta;
	}

	public class Data
	{
		public string type;
		public string id;
		public string url;
		public string image_original_url;
		public string image_url;
		public string image_mp4_url;
		public string image_frames;
		public string image_width;
		public string image_height;
		public string fixed_height_downsampled_url;
		public string fixed_height_downsampled_width;
		public string fixed_height_downsampled_height;
		public string fixed_width_downsampled_url;
		public string fixed_width_downsampled_width;
		public string fixed_width_downsampled_height;
		public string fixed_height_small_url;
		public string fixed_height_small_still_url;
		public string fixed_height_small_width;
		public string fixed_height_small_height;
		public string fixed_width_small_url;
		public string fixed_width_small_still_url;
		public string fixed_width_small_width;
		public string fixed_width_small_height;
		public string username;
		public string caption;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}
	
public class GiphyTranslate
{
	public class Response
	{
		public Data data;
		public Meta meta;
	}

	public class FixedHeightStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class OriginalStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedWidth
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmallStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedHeightDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class Preview
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedHeightSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Downsized
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedLarge
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmallStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class PreviewWebp
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedWidthSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedSmall
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedWidthDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedMedium
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Original
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string frames;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeight
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class Looping
	{
		public string mp4;
		public string mp4_size;
	}

	public class OriginalMp4
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class PreviewGif
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Images
	{
		public FixedHeightStill fixed_height_still;
		public OriginalStill original_still;
		public FixedWidth fixed_width;
		public FixedHeightSmallStill fixed_height_small_still;
		public FixedHeightDownsampled fixed_height_downsampled;
		public Preview preview;
		public FixedHeightSmall fixed_height_small;
		public DownsizedStill downsized_still;
		public Downsized downsized;
		public DownsizedLarge downsized_large;
		public FixedWidthSmallStill fixed_width_small_still;
		public PreviewWebp preview_webp;
		public FixedWidthStill fixed_width_still;
		public FixedWidthSmall fixed_width_small;
		public DownsizedSmall downsized_small;
		public FixedWidthDownsampled fixed_width_downsampled;
		public DownsizedMedium downsized_medium;
		public Original original;
		public FixedHeight fixed_height;
		public Looping looping;
		public OriginalMp4 original_mp4;
		public PreviewGif preview_gif;
	}

	public class Data
	{
		public string type;
		public string id;
		public string slug;
		public string url;
		public string bitly_gif_url;
		public string bitly_url;
		public string embed_url;
		public string username;
		public string source;
		public string rating;
		public string content_url;
		public string source_tld;
		public string source_post_url;
		public int is_indexable;
		public string import_datetime;
		public string trending_datetime;
		public Images images;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}

}
	
public class GiphyTrending
{
	public class Response
	{
		public List<Datum> data;
		public Pagination pagination;
		public Meta meta;
	}

	public class User
	{
		public string avatar_url;
		public string banner_url;
		public string profile_url;
		public string username;
		public string display_name;
		public string twitter;
	}

	public class FixedHeightStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class OriginalStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedWidth
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmallStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedHeightDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class Preview
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedHeightSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Downsized
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedLarge
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmallStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class PreviewWebp
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthStill
	{
		public string url;
		public string width;
		public string height;
	}

	public class FixedWidthSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedSmall
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedWidthDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedMedium
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Original
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string frames;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeight
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class Looping
	{
		public string mp4;
		public string mp4_size;
	}

	public class OriginalMp4
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class PreviewGif
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Images
	{
		public FixedHeightStill fixed_height_still;
		public OriginalStill original_still;
		public FixedWidth fixed_width;
		public FixedHeightSmallStill fixed_height_small_still;
		public FixedHeightDownsampled fixed_height_downsampled;
		public Preview preview;
		public FixedHeightSmall fixed_height_small;
		public DownsizedStill downsized_still;
		public Downsized downsized;
		public DownsizedLarge downsized_large;
		public FixedWidthSmallStill fixed_width_small_still;
		public PreviewWebp preview_webp;
		public FixedWidthStill fixed_width_still;
		public FixedWidthSmall fixed_width_small;
		public DownsizedSmall downsized_small;
		public FixedWidthDownsampled fixed_width_downsampled;
		public DownsizedMedium downsized_medium;
		public Original original;
		public FixedHeight fixed_height;
		public Looping looping;
		public OriginalMp4 original_mp4;
		public PreviewGif preview_gif;
	}

	public class Datum
	{
		public string type;
		public string id;
		public string slug;
		public string url;
		public string bitly_gif_url;
		public string bitly_url;
		public string embed_url;
		public string username;
		public string source;
		public string rating;
		public string content_url;
		public string source_tld;
		public string source_post_url;
		public int is_indexable;
		public string import_datetime;
		public string trending_datetime;
		public User user;
		public Images images;
	}

	public class Pagination
	{
		public int count;
		public int offset;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}
#endregion

#region ------ Sticker Json Object ------
public class GiphyStickerSearch
{
	public class Response
	{
		public List<Datum> data;
		public Pagination pagination;
		public Meta meta;
	}

	public class FixedHeightStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class OriginalStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidth
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmallStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedHeightDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class Preview
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedHeightSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Downsized
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedLarge
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmallStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class PreviewWebp
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedSmall
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedWidthDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedMedium
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Original
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string frames;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
		public string hash;
	}

	public class FixedHeight
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class Looping
	{
		public string mp4;
		public string mp4_size;
	}

	public class OriginalMp4
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class PreviewGif
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Images
	{
		public FixedHeightStill fixed_height_still;
		public OriginalStill original_still;
		public FixedWidth fixed_width;
		public FixedHeightSmallStill fixed_height_small_still;
		public FixedHeightDownsampled fixed_height_downsampled;
		public Preview preview;
		public FixedHeightSmall fixed_height_small;
		public DownsizedStill downsized_still;
		public Downsized downsized;
		public DownsizedLarge downsized_large;
		public FixedWidthSmallStill fixed_width_small_still;
		public PreviewWebp preview_webp;
		public FixedWidthStill fixed_width_still;
		public FixedWidthSmall fixed_width_small;
		public DownsizedSmall downsized_small;
		public FixedWidthDownsampled fixed_width_downsampled;
		public DownsizedMedium downsized_medium;
		public Original original;
		public FixedHeight fixed_height;
		public Looping looping;
		public OriginalMp4 original_mp4;
		public PreviewGif preview_gif;
	}

	public class Datum
	{
		public string type;
		public string id;
		public string slug;
		public string url;
		public string bitly_gif_url;
		public string bitly_url;
		public string embed_url;
		public string username;
		public string source;
		public string rating;
		public string content_url;
		public string source_tld;
		public string source_post_url;
		public int is_indexable;
		public string import_datetime;
		public string trending_datetime;
		public Images images;
	}

	public class Pagination
	{
		public int total_count;
		public int count;
		public int offset;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}

public class GiphyStickerRandom
{
	public class Response
	{
		public Data data;
		public Meta meta;
	}

	public class Data
	{
		public string type;
		public string id;
		public string url;
		public string image_original_url;
		public string image_url;
		public string image_mp4_url;
		public string image_frames;
		public string image_width;
		public string image_height;
		public string fixed_height_downsampled_url;
		public string fixed_height_downsampled_width;
		public string fixed_height_downsampled_height;
		public string fixed_width_downsampled_url;
		public string fixed_width_downsampled_width;
		public string fixed_width_downsampled_height;
		public string fixed_height_small_url;
		public string fixed_height_small_still_url;
		public string fixed_height_small_width;
		public string fixed_height_small_height;
		public string fixed_width_small_url;
		public string fixed_width_small_still_url;
		public string fixed_width_small_width;
		public string fixed_width_small_height;
		public string username;
		public string caption;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}

public class GiphyStickerTranslate
{
	public class Response
	{
		public Data data;
		public Meta meta;
	}

	public class FixedHeightStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class OriginalStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidth
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmallStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedHeightDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class Preview
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedHeightSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Downsized
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedLarge
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmallStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class PreviewWebp
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedSmall
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedWidthDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedMedium
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Original
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string frames;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
		public string hash;
	}

	public class FixedHeight
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class Looping
	{
		public string mp4;
		public string mp4_size;
	}

	public class OriginalMp4
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class PreviewGif
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Images
	{
		public FixedHeightStill fixed_height_still;
		public OriginalStill original_still;
		public FixedWidth fixed_width;
		public FixedHeightSmallStill fixed_height_small_still;
		public FixedHeightDownsampled fixed_height_downsampled;
		public Preview preview;
		public FixedHeightSmall fixed_height_small;
		public DownsizedStill downsized_still;
		public Downsized downsized;
		public DownsizedLarge downsized_large;
		public FixedWidthSmallStill fixed_width_small_still;
		public PreviewWebp preview_webp;
		public FixedWidthStill fixed_width_still;
		public FixedWidthSmall fixed_width_small;
		public DownsizedSmall downsized_small;
		public FixedWidthDownsampled fixed_width_downsampled;
		public DownsizedMedium downsized_medium;
		public Original original;
		public FixedHeight fixed_height;
		public Looping looping;
		public OriginalMp4 original_mp4;
		public PreviewGif preview_gif;
	}

	public class Data
	{
		public string type;
		public string id;
		public string slug;
		public string url;
		public string bitly_gif_url;
		public string bitly_url;
		public string embed_url;
		public string username;
		public string source;
		public string rating;
		public string content_url;
		public string source_tld;
		public string source_post_url;
		public int is_indexable;
		public string import_datetime;
		public string trending_datetime;
		public Images images;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}

public class GiphyStickerTrending
{
	public class Response
	{
		public List<Datum> data;
		public Pagination pagination;
		public Meta meta;
	}

	public class User
	{
		public string avatar_url;
		public string banner_url;
		public string profile_url;
		public string username;
		public string display_name;
		public string twitter;
	}

	public class FixedHeightStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class OriginalStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidth
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class FixedHeightSmallStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedHeightDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class Preview
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedHeightSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Downsized
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class DownsizedLarge
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmallStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class PreviewWebp
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthStill
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class FixedWidthSmall
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedSmall
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class FixedWidthDownsampled
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string webp;
		public string webp_size;
	}

	public class DownsizedMedium
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Original
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string frames;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
		public string hash;
	}

	public class FixedHeight
	{
		public string url;
		public string width;
		public string height;
		public string size;
		public string mp4;
		public string mp4_size;
		public string webp;
		public string webp_size;
	}

	public class Looping
	{
		public string mp4;
		public string mp4_size;
	}

	public class OriginalMp4
	{
		public string width;
		public string height;
		public string mp4;
		public string mp4_size;
	}

	public class PreviewGif
	{
		public string url;
		public string width;
		public string height;
		public string size;
	}

	public class Images
	{
		public FixedHeightStill fixed_height_still;
		public OriginalStill original_still;
		public FixedWidth fixed_width;
		public FixedHeightSmallStill fixed_height_small_still;
		public FixedHeightDownsampled fixed_height_downsampled;
		public Preview preview;
		public FixedHeightSmall fixed_height_small;
		public DownsizedStill downsized_still;
		public Downsized downsized;
		public DownsizedLarge downsized_large;
		public FixedWidthSmallStill fixed_width_small_still;
		public PreviewWebp preview_webp;
		public FixedWidthStill fixed_width_still;
		public FixedWidthSmall fixed_width_small;
		public DownsizedSmall downsized_small;
		public FixedWidthDownsampled fixed_width_downsampled;
		public DownsizedMedium downsized_medium;
		public Original original;
		public FixedHeight fixed_height;
		public Looping looping;
		public OriginalMp4 original_mp4;
		public PreviewGif preview_gif;
	}

	public class Datum
	{
		public string type;
		public string id;
		public string slug;
		public string url;
		public string bitly_gif_url;
		public string bitly_url;
		public string embed_url;
		public string username;
		public string source;
		public string rating;
		public string content_url;
		public string source_tld;
		public string source_post_url;
		public int is_indexable;
		public string import_datetime;
		public string trending_datetime;
		public User user;
		public Images images;
	}

	public class Pagination
	{
		public int count;
		public int offset;
	}

	public class Meta
	{
		public int status;
		public string msg;
		public string response_id;
	}
}
#endregion
