Imports System.Drawing.Image

Public Interface IArtistBio
    ReadOnly Property Name As String
    ReadOnly Property Summary As String
    ReadOnly Property Biography As String
    ReadOnly Property PictureLocation As String ' can be url or filepath
End Interface
