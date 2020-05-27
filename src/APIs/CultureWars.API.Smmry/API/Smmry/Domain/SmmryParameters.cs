using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CultureWars.API.Smmry.Attributes;

namespace CultureWars.API.Smmry.Domain
{
	public class SmmryParameters 
		: Dictionary<string, object>
	{
		/// <summary>
		/// The Smmry API Key.
		/// </summary>
		[SmmryParameter("SM_API_KEY", true)]
		public string ApiKey
		{
			get => (string)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = value;
		}

		///// <summary>
		///// The text input in which to summmarize.
		///// </summary>
		//[SmmryParameter("SM_API_INPUT", true)]
		//public string TextInput
		//{
		//	get => (string)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
		//	set => this[GetSmmryParameterName()] = value;
		//}

		/// <summary>
		/// The webpage in which to summarize.
		/// </summary>
		[SmmryParameter("SM_URL", true)]
		public string Url
		{
			get => (string)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = value;
		}

		/// <summary>
		/// The number of sentences to return.
		/// </summary>
		/// <remarks>
		/// The default number of sentences to return is 7.
		/// </remarks>
		[SmmryParameter("SM_LENGTH", true)]
		public int? SentenceCount
		{
			get => (int?)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = value;
		}

		/// <summary>
		/// The number of keywords to return.
		/// </summary>
		[SmmryParameter("SM_KEYWORD_COUNT", true)]
		public int? KeywordCount
		{
			get => (int?)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = value;
		}

		/// <summary>
		/// Determines if the string [BREAK] will be inserted between sentences.
		/// </summary>
		/// <remarks>
		/// The default return does not include [BREAK] insertions between sentences.
		/// </remarks>
		[SmmryParameter("SM_WITH_BREAK", false)]
		public bool? IncludeBreaks
		{
			get => (bool?)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = value;
		}

		/// <summary>
		/// Determines if the HTML entities will be converted to their applicable chars.
		/// </summary>
		/// <remarks>
		/// The default return does not encode HTML entities.
		/// </remarks>
		[SmmryParameter("SM_WITH_ENCODE", false)]
		public bool? Encode
		{
			get => (bool?)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = value;
		}

		/// <summary>
		/// Returns summary regardless of quality or length.
		/// </summary>
		/// <remarks>
		/// The default return does not ignore length.
		/// </remarks>
		[SmmryParameter("SM_IGNORE_LENGTH", false)]
		public bool? IgnoreLength
		{
			get => (bool?)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = value;
		}

		/// <summary>
		/// Determines if Sentences with quotations will be included.
		/// </summary>
		/// <remarks>
		/// The default return will include quotations.
		/// </remarks>
		/// <remarks>
		/// The value is inversed in this property's setter due to inconsistent naming from the API.
		/// </remarks>
		[SmmryParameter("SM_QUOTE_AVOID", false)]
		public bool? IncludeQuotes
		{
			get => (bool?)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = !value;
		}

		/// <summary>
		/// Determines if the summary should include questions or not.
		/// </summary>
		/// <remarks>
		/// The default will include questions.
		/// </remarks>
		/// <remarks>
		/// The value is inversed in this property's setter due to inconsistent naming from the API.
		/// </remarks>
		[SmmryParameter("SM_QUESTION_AVOID", false)]
		public bool? IncludeQuestion
		{
			get => (bool?)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = !value;
		}

		/// <summary>
		/// Determines if the summary should include sentences with exclamation marks or not.
		/// </summary>
		/// <remarks>
		/// The default will include sentences with exclamation marks.
		/// </remarks>
		/// <remarks>
		/// The value is inversed in this property's setter due to inconsistent naming from the API.
		/// </remarks>
		[SmmryParameter("SM_EXCLAMATION_AVOID", false)]
		public bool? IncludeExclamation
		{
			get => (bool?)(TryGetValue(GetSmmryParameterName(), out var result) ? result : null);
			set => this[GetSmmryParameterName()] = !value;
		}


		private string GetSmmryParameterName(
			[CallerMemberName] string propertyName = null)
		{
			return typeof(SmmryParameters)
				.GetTypeInfo()
				.DeclaredProperties.First(t => t.Name == propertyName)
				.GetCustomAttribute<SmmryParameterAttribute>()
				.ToString();
		}


		public override string ToString()
		{
			// Gets all the parameters that require a parameter
			var haveParameters = GetType()
				.GetTypeInfo()
				.DeclaredProperties
				.Where(t => t.GetCustomAttribute<SmmryParameterAttribute>().HasParameter)
				.Select(t => t.GetCustomAttribute<SmmryParameterAttribute>().Name);

			// Gets all the parameters that don't require a parameter
			var doNotHaveParameters = GetType()
				.GetTypeInfo()
				.DeclaredProperties
				.Where(t => !t.GetCustomAttribute<SmmryParameterAttribute>().HasParameter)
				.Select(t => t.GetCustomAttribute<SmmryParameterAttribute>().Name);

			// Skips SM_URL and the ones without a parameter because SM_URL should be last
			var urlWithParams = this.Where(
					t => t.Key != "SM_URL" 
						&& haveParameters.Any(p => t.Key == p))
				.Select(i => $"{i.Key}={i.Value}");

			var urlWithoutParams = this.Where(
					t => t.Key != "SM_URL" 
						&& doNotHaveParameters.Any(p => p == t.Key) 
						&& (bool)t.Value)
				.Select(i => i.Key);

			var url = $"?{string.Join("&", urlWithParams.Concat(urlWithoutParams))}";
			return string.Concat(url, $"&SM_URL={Url}");
		}
	}
}