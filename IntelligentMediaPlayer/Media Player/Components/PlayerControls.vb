Imports WMPLib
Imports AxWMPLib

Partial Public Class MusicPlayer
    Public Class PlayerControls
        Dim myParentPlayer As MusicPlayer
        Dim myWrappedControls As IWMPControls

        Public Sub New(ByRef parentPlayer As MusicPlayer)
            myParentPlayer = parentPlayer
            myWrappedControls = parentPlayer.myPlayer.Ctlcontrols
        End Sub

        Public Property currentItem As Media
            Get
                Return myWrappedControls.currentItem
            End Get
            Set(ByVal value As Media)
                myWrappedControls.currentItem = value
            End Set
        End Property

        Public Property currentMarker As Integer
            Get
                Return myWrappedControls.currentMarker
            End Get
            Set(ByVal value As Integer)
                myWrappedControls.currentMarker = value
            End Set
        End Property

        Public Property currentPosition As Double
            Get
                Return myWrappedControls.currentPosition
            End Get
            Set(ByVal value As Double)
                myWrappedControls.currentPosition = value
            End Set
        End Property

        Public ReadOnly Property currentPositionString As String
            Get
                Return myWrappedControls.currentPositionString
            End Get
        End Property

        Public Sub fastForward()
            myWrappedControls.fastForward()
        End Sub

        Public Sub fastReverse()
            myWrappedControls.fastReverse()
        End Sub

        Public ReadOnly Property isAvailable(ByVal bstrItem As String) As Boolean
            Get
                Return myWrappedControls.isAvailable(bstrItem)
            End Get
        End Property

        Public Sub [next]()
            myWrappedControls.next()
        End Sub

        Public Sub pause()
            myWrappedControls.pause()
        End Sub

        Public Sub play()
            myWrappedControls.play()
        End Sub

        Public Sub playItem(ByVal pIWMPMedia As Media)
            myWrappedControls.playItem(pIWMPMedia.GetMyWrappedMediaItem)
        End Sub

        Public Sub previous()
            myWrappedControls.previous()
        End Sub

        Public Sub [stop]()
            myWrappedControls.stop()
        End Sub

    End Class
End Class