﻿Imports AxWMPLib
Imports WMPLib
Imports System.Threading
Imports System.Windows


Public Class MusicLibraryStats

    Private mediaPlayerCore As New WMPLib.WindowsMediaPlayerClass


    Public Sub New()

    End Sub

    Public Function HowManyTracksByArtist(ByVal artist As String) As UInteger
        Dim count As UInteger = 0

        Dim mc As WMPLib.IWMPMediaCollection2 = mediaPlayerCore.mediaCollection
        Dim query As WMPLib.IWMPQuery = mc.createQuery()
        Dim attributeName As String = "Artist"
        query.addCondition(attributeName, "Equals", artist)
        Dim result As IWMPPlaylist = mc.getPlaylistByQuery(query, "audio", "", False)
        count = result.count

        Return count
    End Function

    Public Function DoIHaveThisAlbum(ByVal artist As String, ByVal album As String) As Boolean
        Dim count As UInteger = 0

        Dim mc As WMPLib.IWMPMediaCollection2 = mediaPlayerCore.mediaCollection
        Dim query As WMPLib.IWMPQuery = mc.createQuery()
        Dim artistAttributeName As String = "Artist"
        query.addCondition(artistAttributeName, "Equals", artist)
        Dim albumQuery As WMPLib.IWMPQuery = mc.createQuery()
        Dim albumAttributeName As String = "Album"
        query.addCondition(albumAttributeName, "Equals", album)
        Dim result As IWMPPlaylist = mc.getPlaylistByQuery(query, "audio", "", False)
        count = result.count

        Return (count > 0)
    End Function

    Public Function NumberOfMatchesInLibrary(ByVal attributeLookup As Dictionary(Of String, String)) As Integer
        Dim mc As WMPLib.IWMPMediaCollection2 = mediaPlayerCore.mediaCollection
        Dim query As WMPLib.IWMPQuery = mc.createQuery()

        For Each attributeName As String In attributeLookup.Keys
            Dim attributeValue As String = attributeLookup.Item(attributeName)
            query.addCondition(attributeName, "Equals", attributeValue)
        Next

        Dim result As IWMPPlaylist = mc.getPlaylistByQuery(query, "audio", "", False)
        Return result.count
    End Function

End Class

