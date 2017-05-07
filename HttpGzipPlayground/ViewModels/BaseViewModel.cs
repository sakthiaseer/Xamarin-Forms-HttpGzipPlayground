// Author:
// Evgeny Zborovsky
// https://github.com/yuv4ik
//
// Copyright (c) 2017 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using PropertyChanged;

namespace HttpGzipPlayground
{
	[ImplementPropertyChanged]
	public abstract class BaseViewModel
	{
		public string HttpResponse { get; set; }

		public bool ShouldUseModernHttpClient { get; set; }

		protected HttpClient httpClient { get; }
		protected HttpClient modernHttpClient { get; }
		protected abstract string apiEndPoint { get; }

		public BaseViewModel()
		{
			var httpHandler = new HttpClientHandler
			{
				AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
			};

			httpClient = new HttpClient(httpHandler);

			InitHttpClient(httpClient);

			modernHttpClient = new HttpClient(new NativeMessageHandler());
			InitHttpClient(modernHttpClient);
		}

		/// <summary>
		/// <para>Set necessary headers.</para>
		/// </summary>
		/// <param name="client">Client.</param>
		private void InitHttpClient(HttpClient client)
		{
			client.DefaultRequestHeaders.Remove("Accept-Encoding");
			client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
		}

		protected async Task<T> Execute<T>(Func<Task<T>> task)
		{
			try
			{
				return await task();
			}
			catch
			{
				await App.Current.MainPage.DisplayAlert(string.Empty, "Please start the WebApi project.", "OK");
				return default(T);
			}
		}
	}
}
