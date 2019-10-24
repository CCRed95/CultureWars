using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Ccr.Std.Core.Extensions;
using CultureWars.API.GoogleArchives.JsonParsing.Domain.Json;
using JetBrains.Annotations;

namespace CultureWars.API.YouTube.Domain
{
	public partial struct PlaylistType
	{
		/// <summary>
		///		Regular playlist created by a user.
		/// </summary>
		public static readonly PlaylistType Normal = Declare("PL");

		/// <summary>
		///		Mix playlist generated to group similar videos.
		/// </summary>
		public static readonly PlaylistType VideoMix = Declare("RD");

		/// <summary>
		///		Mix playlist generated to group similar videos uploaded by the same channel.
		/// </summary>
		public static readonly PlaylistType ChannelVideoMix = Declare("UL");

		/// <summary>
		///		Playlist generated from channel uploads.
		/// </summary>
		public static readonly PlaylistType ChannelVideos = Declare("UU");

		/// <summary>
		///		Playlist generated from popular channel uploads.
		/// </summary>
		public static readonly PlaylistType PopularChannelVideos = Declare("PU");

		/// <summary>
		///		Playlist generated from automated music videos.
		/// </summary
		public static readonly PlaylistType MusicAlbum = Declare("OL");

		/// <summary>
		///		System playlist for videos liked by a user.
		/// </summary>
		public static readonly PlaylistType LikedVideos = Declare("LL");

		/// <summary>
		///		System playlist for videos favorited by a user.
		/// </summary>
		public static readonly PlaylistType Favorites = Declare("FL");

		/// <summary>
		///		System playlist for videos user added to watch later.
		/// </summary>
		public static readonly PlaylistType WatchLater = Declare("WL");
	}


	/// <summary>
	///		Playlist type.
	/// </summary>
	public partial struct PlaylistType
	{
		[CanBeNull]
		public static PlaylistType[] _cachedPlaylistTypes;


		public string PlaylistIDPrefix { get; }

		public string TypeName { get; }


		private PlaylistType(
			[NotNull] string playlistIDPrefix,
			[CallerMemberName] string memberName = null)
		{
			PlaylistIDPrefix = playlistIDPrefix;
			TypeName = memberName;
		}

		private static PlaylistType Declare(
			[NotNull] string playlistIDPrefix,
			[CallerMemberName] string memberName = null)
		{
			return new PlaylistType(
				playlistIDPrefix,
				memberName);
		}


		public static PlaylistType FromPlaylistTypeName(
			[NotNull] string playlistTypeName)
		{
			playlistTypeName.IsNotNull(nameof(playlistTypeName));

			var playlistTypes = DeclaredPlaylistTypes
				.Where(t => t.TypeName == playlistTypeName)
				.ToArray();

			if (playlistTypes.Length == 0)
				throw new NotSupportedException(
					$"No declared {typeof(PlaylistType).Name.SQuote()} items with the name " +
					$"{playlistTypeName.SQuote()}");

			if (playlistTypes.Length > 1)
				throw new NotSupportedException(
					$"More than one declared {typeof(PlaylistType).Name.SQuote()} items with the name " +
					$"{playlistTypeName.SQuote()}");

			return playlistTypes.Single();
		}


		public static IReadOnlyList<PlaylistType> DeclaredPlaylistTypes
		{
			get => _cachedPlaylistTypes ?? 
				(_cachedPlaylistTypes = _reflectPlaylistTypes());
		}

		private static PlaylistType[] _reflectPlaylistTypes()
		{
			var fieldDeclarations = typeof(PlaylistType)
				.GetFields(
					BindingFlags.Static | BindingFlags.Public)
				.Where(t => t.FieldType == typeof(PlaylistType))
				.Select(t => t.GetValue(null))
				.Cast<PlaylistType>()
				.ToArray();

			return fieldDeclarations;
		}
	}


	public class YouTubePlaylist
	{
		public int YouTubePlaylistID { get; set; }

		public string PlaylistName { get; set; }

		public YouTubeChannel CreatorChannel { get; set; }

		public ICollection<YouTubeVideo> YouTubeVideos { get; set; }
	}

	/// <summary>
	///		Information about a YouTube playlist.
	/// </summary>
	public partial class Playlist
	{
		/// <summary>
		///		ID of this playlist.
		/// </summary>
		[NotNull]
		public string Id { get; }

		/// <summary>
		///		Type of this playlist.
		/// </summary>
		public PlaylistType Type { get; }

		/// <summary>
		///		Author of this playlist.
		/// </summary>
		[NotNull]
		public string Author { get; }

		/// <summary>
		///		Title of this playlist.
		/// </summary>
		[NotNull]
		public string Title { get; }

		/// <summary>
		///		Description of this playlist.
		/// </summary>
		[NotNull]
		public string Description { get; }

		/// <summary>
		///		Statistics of this playlist.
		/// </summary>
		[NotNull]
		public Statistics Statistics { get; }

		/// <summary>
		///		Collection of videos contained in this playlist.
		/// </summary>
		[NotNull, ItemNotNull]
		public IReadOnlyList<YouTubeVideo> Videos { get; }


		/// <summary>
		///		Initializes an instance of <see cref="Playlist"/>.
		/// </summary>
		public Playlist(
			string id,
			string author,
			string title,
			string description,
			Statistics statistics,
			IReadOnlyList<YouTubeVideo> videos)
		{
			id.IsNotNull(nameof(id));
			author.IsNotNull(nameof(author));
			title.IsNotNull(nameof(title));
			description.IsNotNull(nameof(description));
			statistics.IsNotNull(nameof(statistics));
			videos.IsNotNull(nameof(videos));

			Id = id;
			Type = GetPlaylistType(id);
			Title = title;
			Author = author;
			Description = description;
			Statistics = statistics;
			Videos = videos;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"[{Id}] = {Author} - {Title}";
		}
	}

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
			
			if(!_playlistPrefixMapping.TryGetValue(prefix, out var playlistType))
				throw new KeyNotFoundException(
					$"The playlist prefix {prefix.Quote()} has no mapped {typeof(PlaylistType).Name.SQuote()}.");

			return playlistType;
		}
	}
}