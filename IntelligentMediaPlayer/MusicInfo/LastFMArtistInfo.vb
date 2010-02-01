Imports System.Drawing.Image
Imports System.Xml
Imports System.Xml.XPath

Public Class LastFMArtistInfo
    Implements IArtistInfo
    Private myName As String
    Private Const myTemplateURL As String = "http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=[Artist]&api_key=e295ea662af320c44101419cb30cfffe"
    Private myCallResultXML As XmlDocument 'cached because otherwise last.fm will kick this program out for too many calls.

    Public Sub New(ByRef artistName As String)
        myName = artistName
        RetrieveAndStoreResultsFromWebservice()
    End Sub


    Public ReadOnly Property Biography As String Implements IArtistInfo.Biography
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim biographyXpathQuery As String = "//lfm/artist/bio/content"
            Return WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, biographyXpathQuery, 0)
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IArtistInfo.Name
        Get
            Return myName
        End Get
    End Property

    Public ReadOnly Property PictureLocation As String Implements IArtistInfo.PictureLocation
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim imageXpathQuery As String = "//lfm/artist/image"
            Dim imageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, imageXpathQuery, "size", "extralarge")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(imageUrl)
        End Get
    End Property

    Public ReadOnly Property SmallPictureLocation As String Implements IArtistInfo.SmallPictureLocation
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim imageXpathQuery As String = "//lfm/artist/image"
            Dim imageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, imageXpathQuery, "size", "medium")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(imageUrl)
        End Get
    End Property

    Public ReadOnly Property WebLink As String Implements IArtistInfo.WebLink
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim weblinkXpathQuery As String = "//lfm/artist/url"
            Return WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, weblinkXpathQuery, 0)
        End Get
    End Property

    Public ReadOnly Property Summary As String Implements IArtistInfo.Summary
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim summaryXpathQuery As String = "//lfm/artist/bio/summary"
            Return WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, summaryXpathQuery, 0)
        End Get
    End Property

    Public ReadOnly Property SimilarArtists As IArtistInfo() Implements IArtistInfo.SimilarArtists
        Get
            If (myCallResultXML Is Nothing) Then
                Return Nothing
            End If

            Dim artists As ArrayList = New ArrayList()
            Dim similarArtistNameXpathQuery As String = "//lfm/artist/similar/artist/name"
            Dim index As UInteger = 0
            Dim similarArtistName As String = WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, similarArtistNameXpathQuery, index)

            While (Not similarArtistName Is Nothing)
                artists.Add(New LastFMArtistInfo(similarArtistName))
                index += 1
                similarArtistName = WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, similarArtistNameXpathQuery, index)
            End While

            Return artists.ToArray(GetType(IArtistInfo))
        End Get
    End Property

    Public ReadOnly Property Tags As String() Implements IArtistInfo.Tags
        Get
            If (myCallResultXML Is Nothing) Then
                Return Nothing
            End If

            Dim tagsList As ArrayList = New ArrayList()
            Dim tagXpathQuery As String = "//lfm/artist/tags/tag/name"
            Dim index As UInteger = 0
            Dim tagName As String = WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, tagXpathQuery, index)

            While (Not tagName Is Nothing)
                tagsList.Add(tagName)
                index += 1
                tagName = WebServiceClient.GetClient.XMLGetValueAt(myCallResultXML, tagXpathQuery, index)
            End While

            Return tagsList.ToArray(GetType(String))
        End Get
    End Property

    Private Sub RetrieveAndStoreResultsFromWebservice()
        Dim url As String = myTemplateURL.Replace("[Artist]", myName)
        myCallResultXML = WebServiceClient.GetClient.RetrieveResult(url)
    End Sub


End Class
