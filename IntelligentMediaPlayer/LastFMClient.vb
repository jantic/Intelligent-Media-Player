Imports Microsoft.VisualBasic
Imports System.Xml



Public Class LastFMClient

    'sample api call:  http://ws.audioscrobbler.com/2.0/?method=artist.getsimilar&artist=cher&api_key=b25b959554ed7605
    'Your API Key is e295ea662af320c44101419cb30cfffe and your secret is 5e55b1e71d9aa19bac87f051d8059758

    Private Const baseAPIurl As String = "http://ws.audioscrobbler.com/2.0/?method="
    Private Const similarArtistCall As String = "artist.getsimilar&artist="
    Private Const apiTag As String = "&api_key="
    Private Const apiKey As String = "e295ea662af320c44101419cb30cfffe"

    Public Sub New()

    End Sub

    Public Function GetSimilarArtists(ByVal artist As String) As Artist()
        Dim cleanedArtist As String = CleanUpName(artist)
        Dim webserviceCall As String = String.Concat(baseAPIurl, similarArtistCall, cleanedArtist, apiTag, apiKey)
        Dim caller As XmlDocument = New XmlDocument
        caller.Load(webserviceCall)
        Dim root As XmlElement = caller.DocumentElement
        Dim artistNodes As XmlNodeList = root.GetElementsByTagName("artist")

        Dim artistList As ArrayList = New ArrayList

        For Each node As XmlElement In artistNodes
            Dim name As String = node.Item("name").InnerText
            Dim match As Double = Convert.ToDouble(node.Item("match").InnerText)
            artistList.Add(New Artist(name, match))
        Next

        Return artistList.ToArray(GetType(Artist))
    End Function


    Private Function CleanUpName(ByVal name As String) As String
        'just get rid of leading and trailing spaces, for now
        Return name.Trim()
    End Function

End Class
