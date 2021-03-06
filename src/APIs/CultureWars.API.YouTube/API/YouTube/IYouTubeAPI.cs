﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CultureWars.API.YouTube.Domain;

namespace CultureWars.API.YouTube
{
	public class YouTubeAPI
	{

	}

	/// <summary>
	///		Interface for <see cref="YouTubeAPI"/>.
	/// </summary>
	public interface IYouTubeAPI
	{
		#region Video

		/// <summary>
		///		Gets video information by ID.
		/// </summary>
		Task<YouTubeVideo> GetVideoAsync(string videoId);

		/// <summary>
		///		Gets author channel information for given video.
		/// </summary>
		Task<YouTubeChannel> GetVideoAuthorChannelAsync(string videoId);

		/// <summary>
		///		Gets a set of all available media stream infos for given video.
		/// </summary>
		//Task<MediaStreamInfoSet> GetVideoMediaStreamInfosAsync(string videoId);

		/// <summary>
		///		Gets all available closed caption track infos for given video.
		/// </summary>
		//Task<IReadOnlyList<ClosedCaptionTrackInfo>> GetVideoClosedCaptionTrackInfosAsync(string videoId);

		#endregion

		#region Playlist

		/// <summary>
		/// Gets playlist information by ID.
		/// The video list is truncated at given number of pages (1 page ≤ 200 videos).
		/// </summary>
		//Task<Playlist> GetPlaylistAsync(string playlistId, int maxPages);

		/// <summary>
		/// Gets playlist information by ID.
		/// </summary>
		//Task<Playlist> GetPlaylistAsync(string playlistId);

		#endregion

		#region Search

		/// <summary>
		/// Searches videos using given query.
		/// The video list is truncated at given number of pages (1 page ≤ 20 videos).
		/// </summary>
		Task<IReadOnlyList<YouTubeVideo>> SearchVideosAsync(string query, int maxPages);

		/// <summary>
		/// Searches videos using given query.
		/// </summary>
		Task<IReadOnlyList<YouTubeVideo>> SearchVideosAsync(string query);

		#endregion

		#region Channel

		/// <summary>
		/// Gets channel ID by username.
		/// </summary>
		Task<string> GetChannelIdAsync(string username);

		/// <summary>
		/// Gets channel information by ID.
		/// </summary>
		Task<YouTubeChannel> GetChannelAsync(string channelId);

		/// <summary>
		/// Gets videos uploaded by channel with given ID.
		/// The video list is truncated at given number of pages (1 page ≤ 200 videos).
		/// </summary>
		Task<IReadOnlyList<YouTubeVideo>> GetChannelUploadsAsync(string channelId, int maxPages);

		/// <summary>
		/// Gets videos uploaded by channel with given ID.
		/// </summary>
		Task<IReadOnlyList<YouTubeVideo>> GetChannelUploadsAsync(string channelId);

		#endregion

		#region MediaStream

		/// <summary>
		/// Gets the media stream associated with given metadata.
		/// </summary>
		//Task<MediaStream> GetMediaStreamAsync(MediaStreamInfo info);

		/// <summary>
		/// Downloads the stream associated with given metadata to the output stream.
		/// </summary>
//		Task DownloadMediaStreamAsync(MediaStreamInfo info, Stream output,
	//			IProgress<double> progress = null, CancellationToken cancellationToken = default);

#if NETSTANDARD2_0 || NETSTANDARD2_1 || NET45

		/// <summary>
		/// Downloads the stream associated with given metadata to a file.
		/// </summary>
		Task DownloadMediaStreamAsync(MediaStreamInfo info, string filePath,
				IProgress<double> progress = null, CancellationToken cancellationToken = default);

#endif

		#endregion

		#region ClosedCaptionTrack

		/// <summary>
		/// Gets the closed caption track associated with given metadata.
		/// </summary>
		//	Task<ClosedCaptionTrack> GetClosedCaptionTrackAsync(ClosedCaptionTrackInfo info);

		/// <summary>
		/// Downloads the closed caption track associated with given metadata to the output stream.
		/// </summary>
		//	Task DownloadClosedCaptionTrackAsync(ClosedCaptionTrackInfo info, Stream output,
		//		IProgress<double> progress = null, CancellationToken cancellationToken = default);

#if NETSTANDARD2_0 || NETSTANDARD2_1 || NET45

		/// <summary>
		/// Downloads the closed caption track associated with given metadata to a file.
		/// </summary>
		Task DownloadClosedCaptionTrackAsync(ClosedCaptionTrackInfo info, string filePath,
				IProgress<double> progress = null, CancellationToken cancellationToken = default);

#endif

		#endregion
	}
}
