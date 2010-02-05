Partial Public Class PlaylistManager

    Public Class PlaylistModifierInput
        Private myDisplayName As String
        Private myValue As String = ""
        Private myID As UInteger

        Public Sub New(ByVal theDisplayName As String, ByVal theID As UInteger, Optional ByVal theValue As String = "")
            myDisplayName = theDisplayName
            myID = theID
            myValue = theValue
        End Sub

        Public ReadOnly Property DisplayName As String
            Get
                Return myDisplayName
            End Get

        End Property
        Public Property Value As String
            Get
                Return myValue
            End Get

            Set(ByVal inputValue As String)
                myValue = inputValue
            End Set
        End Property

        Public ReadOnly Property ID As UInteger
            Get
                Return myID
            End Get
        End Property

    End Class

End Class
