Imports System.Threading
Imports System.Windows


Public Class MusicLibraryStats

    Public Sub New()

    End Sub

    Public Function HowManyTracksByArtist(ByVal artist As String) As UInteger
        Dim count As UInteger = 0

        Dim mc As MediaCollection = MediaCollection.GetMediaCollection()
        Dim query As New MediaCollection.Query()
        Dim attributeName As String = "author"
        query.addCondition(attributeName, "Equals", artist)
        Dim result As Playlist = mc.GetPlaylistByQuery(query)
        count = result.count

        Return count
    End Function

    Public Function DoIHaveThisAlbum(ByVal artist As String, ByVal album As String) As Boolean
        Dim count As UInteger = 0

        Dim mc As MediaCollection = MediaCollection.GetMediaCollection()
        Dim query As New MediaCollection.Query()
        Dim artistAttributeName As String = "author"
        query.addCondition(artistAttributeName, "Equals", artist)
        Dim albumQuery As New MediaCollection.Query()
        Dim albumAttributeName As String = "albumid"
        query.addCondition(albumAttributeName, "Equals", album)
        Dim result As Playlist = mc.GetPlaylistByQuery(query)
        count = result.count

        Return (count > 0)
    End Function

    Public Function NumberOfMatchesInLibrary(ByVal attributeLookup As Dictionary(Of String, String)) As Integer
        Dim mc As MediaCollection = MediaCollection.GetMediaCollection()
        Dim query As New MediaCollection.Query()

        For Each attributeName As String In attributeLookup.Keys
            Dim attributeValue As String = attributeLookup.Item(attributeName)
            query.addCondition(attributeName, "Equals", attributeValue)
        Next

        Dim result As Playlist = mc.getPlaylistByQuery(query)
        Return result.count
    End Function

End Class

