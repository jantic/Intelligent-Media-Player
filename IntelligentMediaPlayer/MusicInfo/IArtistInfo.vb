Imports System.Drawing.Image

Public Interface IArtistInfo
    ReadOnly Property Name As String
    ReadOnly Property Summary As String
    ReadOnly Property Biography As String
    ReadOnly Property PictureLocation As String ' can be url or filepath
    ReadOnly Property SmallPictureLocation As String ' can be url or filepath
    ReadOnly Property SimilarArtists As IArtistInfo()
    ReadOnly Property WebLink As String
    ReadOnly Property Tags As String()
End Interface
