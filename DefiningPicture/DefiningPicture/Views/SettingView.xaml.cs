using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DefiningPicture
{
	public partial class SettingView : ContentPage
	{
		public SettingView ()
		{
			InitializeComponent ();

            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html =
                "<html><head>"
                    + "<style>"
                        + "li {"
                            + "font-size: 13px;"
                        + "}"
                    + "</style>"
                + "</head>"
                + "<body>"
                    + "<ul type='square' style='-webkit-padding-start:20px;'>"
                        + "<li>Go To \"https://azure.microsoft.com/en-gb/services/cognitive-services/computer-vision/\" and Try the Computer Vision API.</li>"
                        + "<li>Get API Key and Sign In (Trial Version API Key Will Active For 7 Days).</li>"
                        + "<li>Put The Endpoints You Get Into The Computer Vision Root (v1.0) and Key (Key 1).</li>"
                        + "<li>Define Picture Sometimes Failed, But If Failed Persist Check Spelling of Your Root and Key.</li>"
                    + "</ul>"
                + "</body></html>";
            howToWebView.Source = htmlSource;

            BindingContext = new SettingViewModel();           
        }
	}
}