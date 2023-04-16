# CacheProxyMockServer

a cross-platform self-hosted proxy server, hosted in desktop app used to facilitate development and testing of mobile, web and desktop apps by mocking rest APIs

Useful for caching the requests for fast testing or to be able test the app functionality and design even if the server is down

Implemented in .Net 6, UI created using [AvaloniaUI]([Avalonia UI - Home](https://www.avaloniaui.net/)) framework

## How to use

- it's a desktop app, launches a self-hosted server on port 1234, that's why it must run as administrator 
- configure the Browser app to preceed every url with 'http://localhost:1234?url=' 
- cache is not in use by default, enable it by right clicking the icon in the system tray or by the button on the form


### TO DO:
* Tray Icon
* Server renames
* Delete history
* Theming