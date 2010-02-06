Imports AxWMPLib
Imports WMPLib
Imports System.Threading
Imports System.Windows


Public Class MusicLibraryStats
    Public Sub New()

    End Sub

    Delegate Function HowManyTracksByArtistsDELEGATE(ByVal artist As String, ByVal player As AxWindowsMediaPlayer) As UInteger  'needed for thread safety

    Public Function HowManyTracksByArtist(ByVal artist As String, ByVal player As AxWindowsMediaPlayer) As UInteger
        Dim count As UInteger = 0

        If (player.InvokeRequired) Then 'needed for thread safety
            Dim d As New HowManyTracksByArtistsDELEGATE(AddressOf HowManyTracksByArtist)
            count = DirectCast(player.Invoke(d, artist, player), UInteger)
        Else
            Dim mc As WMPLib.IWMPMediaCollection2 = player.mediaCollection
            Dim query As WMPLib.IWMPQuery = mc.createQuery()
            Dim attributeName As String = "Artist"
            query.addCondition(attributeName, "Equals", artist)
            Dim result As IWMPPlaylist = mc.getPlaylistByQuery(query, "audio", "", False)
            count = result.count
        End If

        Return count
    End Function

    Delegate Function DoIHaveThisAlbumDELEGATE(ByVal artist As String, ByVal album As String, ByRef player As AxWindowsMediaPlayer) As Boolean 'needed for thread safety

    Public Function DoIHaveThisAlbum(ByVal artist As String, ByVal album As String, ByRef player As AxWindowsMediaPlayer) As Boolean
        Dim count As UInteger = 0

        If (player.InvokeRequired) Then 'needed for thread safety
            Dim d As New DoIHaveThisAlbumDELEGATE(AddressOf DoIHaveThisAlbum)
            count = DirectCast(player.Invoke(d, artist, album, player), Boolean)
        Else
            Dim mc As WMPLib.IWMPMediaCollection2 = player.mediaCollection
            Dim query As WMPLib.IWMPQuery = mc.createQuery()
            Dim artistAttributeName As String = "Artist"
            query.addCondition(artistAttributeName, "Equals", artist)
            Dim albumQuery As WMPLib.IWMPQuery = mc.createQuery()
            Dim albumAttributeName As String = "Album"
            query.addCondition(albumAttributeName, "Equals", album)
            Dim result As IWMPPlaylist = mc.getPlaylistByQuery(query, "audio", "", False)
            count = result.count
        End If

        Return (count > 0)
    End Function


End Class

