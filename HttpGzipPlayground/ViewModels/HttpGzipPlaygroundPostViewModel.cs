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
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace HttpGzipPlayground
{
	public class HttpGzipPlaygroundPostViewModel : BaseViewModel
	{
		public ICommand PostHttpRequestCmd { get; }

		protected override string apiEndPoint => "http://localhost:5000/values/upload";

		public HttpGzipPlaygroundPostViewModel()
		{
			PostHttpRequestCmd = new Command(async () =>
			{
				var jsonContent = new JsonContent(new List<string> { "a", "b", "c", "d", "e", "f" });
				HttpResponse = string.Empty;
				var response = await Execute(() => ShouldUseModernHttpClient ?
													  modernHttpClient.PostAsync(apiEndPoint, jsonContent) :
																   httpClient.PostAsync(apiEndPoint, jsonContent));
				if (response == null)
					return;
				
				HttpResponse = await response.Content.ReadAsStringAsync();
			});
		}
	}
}
