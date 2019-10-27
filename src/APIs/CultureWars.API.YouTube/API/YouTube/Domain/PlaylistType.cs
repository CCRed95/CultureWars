using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Ccr.Std.Core.Extensions;
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


		public static PlaylistType FromName(
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

		public static PlaylistType FromPrefix(
			[NotNull] string playlistPrefix)
		{
			playlistPrefix.IsNotNull(nameof(playlistPrefix));

			if (!PrefixToPlaylistTypeMapping.TryGetValue(playlistPrefix, out var playlistType))
				throw new KeyNotFoundException(
					$"");

			return playlistType;
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

		private static IReadOnlyDictionary<string, PlaylistType> PrefixToPlaylistTypeMapping
		{
			get => DeclaredPlaylistTypes.ToDictionary(
				t => t.PlaylistIDPrefix,
				t => t);
		}
	}
}

/*			;
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

			return playlistTypes.Single();*/
