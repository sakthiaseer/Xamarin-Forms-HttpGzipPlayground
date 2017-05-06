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

using System.Net.Http;
using System.Windows.Input;
using ModernHttpClient;
using PropertyChanged;
using Xamarin.Forms;

namespace HttpGzipPlayground
{
	[ImplementPropertyChanged]
	public class HttpGzipPlaygrondViewModel
	{
		public string HttpResponse { get; set; }

		public ICommand SendHttpRequestCmd { get; }

		public bool ShouldUseModernHttpClient { get; set; }

		private HttpClient httpClient { get; }
		private HttpClient modernHttpClient { get; }

		private static string url = "http://localhost:5000/values";

		public HttpGzipPlaygrondViewModel()
		{
			httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Remove("Accept-Encoding");
			httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");

			modernHttpClient = new HttpClient(new NativeMessageHandler());
			modernHttpClient.DefaultRequestHeaders.Remove("Accept-Encoding");
			modernHttpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");

			SendHttpRequestCmd = new Command(async () =>
			{
				HttpResponse = string.Empty;
				HttpResponse = await (ShouldUseModernHttpClient ?
					modernHttpClient.GetStringAsync(url) :
					  httpClient.GetStringAsync(url));
			});
		}
	}
}
