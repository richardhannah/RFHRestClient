using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RFHRestClient.CommonMediaTypes
{
    public static class MIMEType
    {

        public static MediaTypeWithQualityHeaderValue APPLICATION_JSON {
            get
            {
                return new MediaTypeWithQualityHeaderValue("application/json"); 
            }
        }

        public static MediaTypeWithQualityHeaderValue TEXT_PLAIN
        {
            get
            {
                return new MediaTypeWithQualityHeaderValue("text/plain");
            }
        }

        public static MediaTypeWithQualityHeaderValue TEXT_HTML
        {
            get
            {
                return new MediaTypeWithQualityHeaderValue("text/html");
            }
        }

        public static MediaTypeWithQualityHeaderValue TEXT_CSS
        {
            get
            {
                return new MediaTypeWithQualityHeaderValue("text/css");
            }
        }

        public static MediaTypeWithQualityHeaderValue APPLICATION_XML
        {
            get
            {
                return new MediaTypeWithQualityHeaderValue("application/xml");
            }
        }
    }
}

//TODO
/*
List Of Mime Types
Text
PHP – text/html
HTML – text/html
HTM – text/html
CSS – text/css
Multipart
– multipart/mixed
Application
PDF – application/pdf
ZIP – application/zip
JAVASCRIPT – application/javascript
JSON – application/json
XML - application/xml
Image
GIF – image/gif
JPEG – image/jpg
JPG – image/jpg
PNG – image/png
Video
MPEG-1 – video/mpeg
MP4 Video – video/mp4
Quicktime – video/quicktime
WMV – video/x-ms-wmv
3GP – video/3gpp
Audio
MP3 – audio/mpeg
MP4 – audio/mp4
Ogg Vorbis – audio/ogg
*/