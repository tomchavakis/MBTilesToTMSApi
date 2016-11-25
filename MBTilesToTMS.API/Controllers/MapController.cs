using MBTilesToTMS.API.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MBTilesToTMS.API.Controllers
{
    public class MapController : ApiController
    {
        [Route("api/Map/{level}/{col}/{row}")]
        public HttpResponseMessage Get(int level, int col, int row)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/countries-raster.mbtiles");
            var connectionString = string.Format("Data Source={0}", fullPath);
            var mbTileProvider = new MbTileProvider(connectionString);
            MemoryStream image = mbTileProvider.GetTile(level, col, row);
            response.Content = new StreamContent(image); // this file stream will be closed by lower layers of web api for you once the response is completed.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return response;
        }
    }
}

