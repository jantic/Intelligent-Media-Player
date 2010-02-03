Imports System.Xml

Public Class LastFMArtistNameFace
    Implements IArtistNameFace

    Private mySimilarArtistXML As XmlElement

    Private Const artistNameXpathQuery As String = "name"
    Private Const imageXpathQuery As String = "image"

    Public Sub New(ByVal similarArtistXML As XMLElement)
        mySimilarArtistXML = similarArtistXML
    End Sub

    Public ReadOnly Property Name As String Implements IArtistNameFace.Name
        Get
            Dim theName As String = WebServiceClient.GetClient.XMLGetValueAt(mySimilarArtistXML, artistNameXpathQuery, 0)
            Return theName
        End Get
    End Property

    Public ReadOnly Property LargePictureLocation As String Implements IArtistNameFace.LargePictureLocation
        Get
            Dim largeImageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(mySimilarArtistXML, imageXpathQuery, "size", "large")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(largeImageUrl)
        End Get
    End Property

    Public ReadOnly Property SmallPictureLocation As String Implements IArtistNameFace.SmallPictureLocation
        Get
            Dim smallImageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(mySimilarArtistXML, imageXpathQuery, "size", "medium")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(smallImageUrl)
        End Get
    End Property
End Class
