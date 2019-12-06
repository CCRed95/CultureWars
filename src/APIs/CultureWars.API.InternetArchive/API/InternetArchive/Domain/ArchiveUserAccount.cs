using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using Ccr.Std.Core.Extensions;
using CultureWars.API.Infrastructure;
using CultureWars.API.InternetArchive.Interpreters;
using JetBrains.Annotations;

namespace CultureWars.API.InternetArchive.Domain
{
	public class InternetArchiveClient
	{
		public InternetArchiveChannel GetChannel(
			string channelName)
		{
			channelName.IsNotNull(nameof(channelName));

			var _requestBuilder = new DomainFragment("https://archive.org");

			var url = _requestBuilder
				.Builder
				.WithPath("details")
				.WithPath($"@{channelName}")
				.Build();

			var context = BrowsingContext.New(
				Configuration.Default.WithDefaultLoader());

			using (var document = context
				.OpenAsync(url)
				.GetAwaiter()
				.GetResult())
			{
				var s = document.QuerySelector(
					"html > body > #wrap > #naWrap > #naWrap > div > ul > li > #user-menu > .img.avatar");

				return new InternetArchiveChannel(
					"",
					"");
			}

			//using (var httpClient = new HttpClientWrapper())
			//{
			//	var response = httpClient
			//		.GetContentAsync(url)
			//		.GetAwaiter()
			//		.GetResult();

			//	var formattedResponse = response;
			//	if (formattedResponse.StartsWith("callback("))
			//	{
			//		formattedResponse = formattedResponse
			//			.Substring("callback(".Length)
			//			.TrimEnd(')');
			//	}

			//	var archiveResponse = JsonConvert
			//		.DeserializeObject<RootObject>(
			//			formattedResponse);

			//	var archiveAlbums = archiveResponse
			//		.Response
			//		.Docs
			//		.Select(
			//			ArchiveAlbumInterpreter.CreateArchiveAlbum);

			//	return archiveAlbums;
			//}
		}
	}

	public class ArchiveUploadGuest
	{
		public string GuestName { get; }
	}


	//public enum ArchiveUploadItemKinds
	//{
	//	MMP3Audio,
	//	MPEG4AudioVisual,
	//	OGGAudioVisual
	//}

	public static class InternetArchiveItemExtensions
	{
		public static string GetItemDownloadPageUrl(
			this InternetArchiveItem @this)
		{
			return $"https://www.archive.org/" +
				$"download/" +
				$"{@this.Identifier}/";
		}

		public static string GetItemThumbnailDownloadPageUrl(
			this InternetArchiveItem @this)
		{
			return $"https://www.archive.org/" +
				$"download/" +
				$"{@this.Identifier}/" +
				$"{@this.Identifier}.thumbs/";
		}

		public static IReadOnlyList<InternetArchiveFile> GetItemFiles(
			this InternetArchiveItem @this)
		{
			return ArchiveFileInterpreter.ScrapeArchiveFiles(@this)
				.ToArray();
		}

		public static IReadOnlyList<InternetArchiveFile> GetItemThumbnailFiles(
			this InternetArchiveItem @this)
		{
			return ArchiveFileInterpreter.ScrapeArchiveThumbnailFiles(@this)
				.Where(t => t.FilePathUrl.EndsWith(".jpg"))
				.ToArray();
		}
	}


	/// <summary>
	///		Represents an Internet Archive Item that contains a set of files and related metadata.
	/// </summary>
	public class InternetArchiveItem
	{
		/// <summary>
		///		Indicates the unique <see cref="InternetArchiveItem"/> Item Identifier.
		/// </summary>
		[NotNull]
		public string Identifier { get; }

		/// <summary>
		///		The <see cref="InternetArchiveItem"/> Title metadata <see cref="string"/>.
		/// </summary>
		[NotNull]
		public string Title { get; }

		public string ChannelName { get; }

		public string Collection { get; }

		public string Creator { get; }

		public DateTime UploadDate { get; }

		public string Description { get; }

		public string Language { get; }

		[NotNull, ItemNotNull]
		public IReadOnlyList<IASubjectTag> SubjectTags { get; }



		public InternetArchiveItem(
			[NotNull] string identifer,
			[NotNull] string title,
			[NotNull] string channelName,
			[NotNull] string collection,
			[NotNull] string creator,
			DateTime uploadDate,
			[NotNull] string description,
			[NotNull] string language,
			[NotNull, ItemNotNull] IEnumerable<IASubjectTag> subjectTags)
		{
			identifer.IsNotNull(nameof(identifer));
			title.IsNotNull(nameof(title));
			channelName.IsNotNull(nameof(channelName));
			collection.IsNotNull(nameof(collection));
			creator.IsNotNull(nameof(creator));
			description.IsNotNull(nameof(description));
			language.IsNotNull(nameof(language));
			subjectTags.IsNotNull(nameof(subjectTags));
			
			Identifier = identifer;
			Title = title;
			ChannelName = channelName;
			Collection = collection;
			Creator = creator;
			UploadDate = uploadDate;
			Description = description;
			Language = language;
			SubjectTags = subjectTags.ToList();
		}
	}


	public class InternetArchiveFile
	{
		public string FileName { get; }
		
		public IAFileKind FileKind { get; }
		
		public string Title { get; }

		public int IndexWithinItem { get; }
		
		public string FilePathUrl { get; }

		public DateTime LastModifiedDate { get; }
		
		public double ApproximateBytes { get; }

		public InternetArchiveItem OwnerArchiveItem { get; }



		public InternetArchiveFile(
			[NotNull] InternetArchiveItem ownerArchiveItem,
			[NotNull] string fileName,
			[NotNull] IAFileKind fileKind,
			[NotNull] string title,
			DateTime lastModifiedDate,
			double approximateBytes,
			int indexWithinItem,
			bool isThumbnailFile = false)
		{
			ownerArchiveItem.IsNotNull(nameof(ownerArchiveItem));
			fileName.IsNotNull(nameof(fileName));
			fileKind.IsNotNull(nameof(fileKind));
			title.IsNotNull(nameof(title));

			OwnerArchiveItem = ownerArchiveItem;
			FileName = fileName;
			FileKind = fileKind;
			Title = title;
			IndexWithinItem = indexWithinItem;

			if (isThumbnailFile)
				FilePathUrl = $"{ownerArchiveItem.GetItemThumbnailDownloadPageUrl()}{fileName}";
			else
				FilePathUrl = $"{ownerArchiveItem.GetItemDownloadPageUrl()}{fileName}";
			
			LastModifiedDate = lastModifiedDate;
			ApproximateBytes = approximateBytes;
		}
	}

	/// <summary>
	///		Information about a Internet Archive channel.
	/// </summary>
	public class InternetArchiveChannel
	{
		/// <summary>
		///		Name of this channel.
		/// </summary>
		[NotNull]
		public string ChannelName { get; }

		/// <summary>
		///		Logo image URL of this channel.
		/// </summary>
		[NotNull]
		public string LogoUrl { get; }

		/// <summary>
		///		Initializes an instance of <see cref="InternetArchiveChannel"/>.
		/// </summary>
		public InternetArchiveChannel(
			[NotNull] string channelName,
			[NotNull] string logoUrl)
		{
			channelName.IsNotNull(nameof(channelName));
			logoUrl.IsNotNull(nameof(logoUrl));
			
			ChannelName = channelName;
			LogoUrl = logoUrl;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return ChannelName;
		}
	}
}




//public string Source { get; }
//public ArchiveUploadItemKinds MediaType { get; }

//public string UploadFileUriPath { get; }
//public string MD5 { get; }

//public string Creator { get; }

//public DateTime? Date { get; set; }

//public DateTime? LastModifiedDate { get; set; }







//public DateTime? OriginalUploadDate { get; }

//public string UploaderAccountName { get; }

//public DateTime? OriginalContentGroupReleaseDateMinimum { get; }

//public DateTime? OriginalContentGroupReleaseDateMaximum { get; }

//public TimeSpan? ContentGroupReleaseDateEntireDuration { get; }




//public string CollectionIdentifier { get; }

//public string UploadCompositeDescription { get; }

//public DateTime? OriginalUploadDate { get; }

//public DateTime? OriginalPublishDate { get; }

//public string UploaderAccountName { get; }


//public IReadOnlyList<ArchiveUploadShowHost> ArchiveUploadShowHosts { get; }

//public IReadOnlyList<ArchiveUploadGuest> ArchiveUploadGuests { get; }


//public TimeSpan? MediaDuration { get; }