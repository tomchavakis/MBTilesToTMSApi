# mbtile file
The mbtile file is stored at the App_Data Folder and i take it from 
https://github.com/klokantech/vector-tiles-sample
# TMS
TMS (Tile Map Service) is a protocol for serving maps as tiles i.e. splitting the map up into a pyramid of images at multiple zoom levels.

http://wiki.openstreetmap.org/wiki/TMS
http://wiki.openstreetmap.org/wiki/Slippy_map_tilenames

# About Mbtiles
The MBTiles specification is an efficient format for storing millions of tiles in a single SQLite database.
You can read more at https://www.mapbox.com/help/an-open-platform/

#Provider
The MBTileProvider read from the sqlite table "tiles" and returns the BLOB data from column "tile_data"

# WebApi
The MapController  Get(int level, int col, int row) function communicates with the sqlite and return the 
approprate image.

# Leaflet
You can use leaflet and make a call to the api controller to get the images back.     

<pre>
L.tileLayer('http://localhost:54389/api/map/{z}/{x}/{y}', { minZoom: 1, maxZoom: 6, opacity: 1 }).addTo(map);
</pre>

# Resources
I use the ConvertGoogleTileToTMSTile formula from
https://geopbs.codeplex.com/