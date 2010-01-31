Imports System.Drawing.Image
Imports System.Xml
Imports System.Xml.XPath

Public Class LastFMArtistBio
    Implements IArtistBio
    Private myName As String
    Private Const myTemplateURL As String = "http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=[Artist]&api_key=e295ea662af320c44101419cb30cfffe"
    Private myCallResultXML As XmlDocument 'cached because otherwise last.fm will kick this program out for too many calls.

    Public Sub New(ByRef artistName As String)
        myName = artistName
        RetrieveAndStoreResultsFromWebservice()
    End Sub


    Public ReadOnly Property Biography As String Implements IArtistBio.Biography
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim biographyXpathQuery As String = "//lfm/artist/bio/content"
            Return WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, biographyXpathQuery, 0)
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IArtistBio.Name
        Get
            Return myName
        End Get
    End Property

    Public ReadOnly Property PictureLocation As String Implements IArtistBio.PictureLocation
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim imageXpathQuery As String = "//lfm/artist/image"
            Dim imageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, imageXpathQuery, "size", "extralarge")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(imageUrl)
        End Get
    End Property

    Public ReadOnly Property Summary As String Implements IArtistBio.Summary
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim summaryXpathQuery As String = "//lfm/artist/bio/summary"
            Return WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, summaryXpathQuery, 0)
        End Get
    End Property

    Private Sub RetrieveAndStoreResultsFromWebservice()
        Dim url As String = myTemplateURL.Replace("[Artist]", myName)
        myCallResultXML = WebServiceClient.GetClient.RetrieveResult(url)
    End Sub


End Class
