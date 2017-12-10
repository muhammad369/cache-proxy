# cache-proxy

a self-hosted proxy server used to facilitate testing of Cordova apps between mobile client and rest server 

## It's useful for

- overcomming CORS and preflight requests especially when it's not easy to configure the server for this, so you can debug on the browser and no need for simulators
- caching the requests for fast testing or to be able test the app functionality and design even if the server is down

## How to use

- it's a windows forms desktop app, launches a self-hosted server on port 1234, that's why it must run as administrator 
- configure the mobile app to preceed every url with 'http://localhost:1234?url=' 
- cache is not in use by default, enable it by right clicking the icon in the system tray or by the button on the form
