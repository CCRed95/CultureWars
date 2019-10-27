using Ccr.Std.Core.Extensions;
using CultureWars.API.GoogleArchives.JsonParsing.Domain.Json;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace CultureWars.API.YouTube.Domain
{
	/// <summary>
	///	Information about a YouTube Playlist.
	/// </summary>
	public class YouTubePlaylist
	{
		/// <summary>
		///	The <see cref="string"/> PlaylistId' of this playlist.
		/// </summary>
		[NotNull]
		public string YouTubePlaylistId { get; }

		/// <summary>
		///	The <see cref="PlaylistType"/> of this playlist.
		/// </summary>
		public PlaylistType Type { get; }

		/// <summary>
		///	The <see cref="string"/> Author of this playlist.
		/// </summary>
		[NotNull]
		public string Author { get; }

		/// <summary>
		///	The <see cref="string"/> Title of this playlist.
		/// </summary>
		[NotNull]
		public string Title { get; }

		/// <summary>
		///	The <see cref="string"/> Description of this playlist.
		/// </summary>
		[NotNull]
		public string Description { get; }

		/// <summary>
		///	The <see cref="Statistics"/> of this playlist.
		/// </summary>
		[NotNull]
		public Statistics Statistics { get; }


		/// <summary>
		///	<see cref="IReadOnlyList{T}"/> of type <see cref="YouTubeVideo"/> containing information
		///	aboutof videos contained in this playlist.
		/// </summary>
		[NotNull, ItemNotNull]
		public IReadOnlyList<YouTubeVideo> Videos { get; }


		/// <summary>
		///	Initializes an instance of <see cref="YouTubePlaylist"/>.
		/// </summary>
		internal YouTubePlaylist(
			string youTubePlaylistId,
			string author,
			string title,
			string description,
			Statistics statistics,
			IReadOnlyList<YouTubeVideo> videos)
		{
			youTubePlaylistId.IsNotNull(nameof(youTubePlaylistId));
			author.IsNotNull(nameof(author));
			title.IsNotNull(nameof(title));
			description.IsNotNull(nameof(description));
			statistics.IsNotNull(nameof(statistics));
			videos.IsNotNull(nameof(videos));

			YouTubePlaylistId = youTubePlaylistId;
			var playlistPrevix = youTubePlaylistId.Substring(0, 2);
			Type = PlaylistType.FromPrefix(playlistPrevix);
			Title = title;
			Author = author;
			Description = description;
			Statistics = statistics;
			Videos = videos;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"[{YouTubePlaylistId}] = {Author} - {Title}";
		}
	}
}


/*
 * 
	public partial class Playlist
	{
		private static readonly IReadOnlyDictionary<string, PlaylistType> _playlistPrefixMapping
			= new Dictionary<string, PlaylistType>
			{
				["PL"] = PlaylistType.Normal,
				["RD"] = PlaylistType.VideoMix,
				["UL"] = PlaylistType.ChannelVideoMix,
				["UU"] = PlaylistType.ChannelVideos,
				["PU"] = PlaylistType.PopularChannelVideos,
				["OL"] = PlaylistType.MusicAlbum,
				["LL"] = PlaylistType.LikedVideos,
				["FL"] = PlaylistType.Favorites,
				["WL"] = PlaylistType.WatchLater,
			};


		/// <summary>
		///		Get playlist type by playlistID.
		/// </summary>
		public static PlaylistType GetPlaylistType(
			[NotNull] string playlistID)
		{
			playlistID.IsNotNull(nameof(playlistID));

			var prefix = playlistID.Substring(0, 2);

			if (!_playlistPrefixMapping.TryGetValue(prefix, out var playlistType))
				throw new KeyNotFoundException(
					$"The playlist prefix {prefix.Quote()} has no mapped {typeof(PlaylistType).Name.SQuote()}.");

			return playlistType;
		}
	}
 */


//public class YouTubePlaylist
//{
//	public int YouTubePlaylistID { get; set; }

//	public string PlaylistName { get; set; }

//	public YouTubeChannel CreatorChannel { get; set; }

//	public ICollection<YouTubeVideo> YouTubeVideos { get; set; }
//}