Imports WMPLib
Imports AxWMPLib

Public Class Media
    Private myWMPMediaToWrap As IWMPMedia
    Private myArtist As String
    Private myReleaseDate As String
    Private myTrack As String
    Private myAlbum As String
    Private myName As String


    Public Sub New(ByRef toWrap As IWMPMedia)
        myWMPMediaToWrap = toWrap
        myArtist = toWrap.getItemInfo("Author")
        myReleaseDate = toWrap.getItemInfo("ReleaseDateYear")
        myTrack = toWrap.getItemInfo("Title")
        myName = myTrack
        myAlbum = toWrap.getItemInfo("AlbumID")
    End Sub

    Public Function GetMyWrappedMediaItem() As IWMPMedia
        Return myWMPMediaToWrap
    End Function

    Public Function getItemInfo(ByVal bstrItemName As String) As String
        Select Case bstrItemName.Trim.ToLower
            Case "artist"
                Return myArtist
            Case "albumid"
                Return myAlbum
            Case "album"
                Return myAlbum
            Case "author"
                Return myArtist
            Case "track"
                Return myTrack
            Case "title"
                Return myTrack
            Case "releasedateyear"
                Return myReleaseDate
        End Select
        Return myWMPMediaToWrap.getItemInfo(bstrItemName)
    End Function

    Public Property name As String
        Get
            Return myName
        End Get
        Set(ByVal value As String)
            myName = value
        End Set
    End Property

    Public ReadOnly Property attributeCount As Integer
        Get
            Return myWMPMediaToWrap.attributeCount
        End Get
    End Property

    Public ReadOnly Property duration As Double
        Get
            Return myWMPMediaToWrap.duration
        End Get
    End Property

    Public ReadOnly Property durationString As String
        Get
            Return myWMPMediaToWrap.durationString
        End Get
    End Property

    Public Function getAttributeName(ByVal lIndex As Integer) As String
        Return myWMPMediaToWrap.getAttributeName(lIndex)
    End Function

    Public Function getItemInfoByAtom(ByVal lAtom As Integer) As String
        Return myWMPMediaToWrap.getItemInfoByAtom(lAtom)
    End Function

    Public Function getMarkerName(ByVal MarkerNum As Integer) As String
        Return myWMPMediaToWrap.getMarkerName(MarkerNum)
    End Function

    Public Function getMarkerTime(ByVal MarkerNum As Integer) As Double
        Return myWMPMediaToWrap.getMarkerTime(MarkerNum)
    End Function

    Public ReadOnly Property imageSourceHeight As Integer
        Get
            Return myWMPMediaToWrap.imageSourceHeight
        End Get
    End Property

    Public ReadOnly Property imageSourceWidth As Integer
        Get
            Return myWMPMediaToWrap.imageSourceWidth
        End Get
    End Property

    Public ReadOnly Property isIdentical(ByVal pIWMPMedia As Media) As Boolean
        Get
            Return myWMPMediaToWrap.isIdentical(pIWMPMedia.GetMyWrappedMediaItem)
        End Get
    End Property

End Class
