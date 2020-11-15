-- Resource Metadata
fx_version 'bodacious'
games { 'gta5' }

author 'Muulfz and Stinx Academmy'
description 'Example resource'
version '1.0.0'

server_script {
    'MuulfzInteroperationServerDebug.net.dll',
    'MuulfzInteroperation.Server.net.dll'
}

client_script {
    'MuulfzInteroperationClientDebug.net.dll',
    'MuulfzInteroperation.Client.net.dll'
}

file {
    'MuulfzInteroperation.Core.dll'
}