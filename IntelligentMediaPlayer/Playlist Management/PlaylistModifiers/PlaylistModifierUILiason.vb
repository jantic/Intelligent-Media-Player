Imports System.IO
Imports System.Xml
Imports System.Xml.XPath

Partial Public Class PlaylistManager

    Public Class PlaylistModifierUILiason
        Private myDisplayName As String = Nothing
        Private myAction As IModifierAction = Nothing
        Private myFilePath As String = Nothing
        Private myInputs As PlaylistModifierInput() = Nothing
        Private myType As ModifierType = Nothing
        Private myModifierKey As String = Nothing
        Private Shared myModifiersDirectory As String = Nothing
        Private Shared myModifierKeyLookup As New Dictionary(Of String, String)

        Public Sub New(ByVal modifiersDirectory As String) 'used to create new meta filters
            myType = ModifierType.Meta
            myAction = New ModifierAction_Add
            myDisplayName = ""
            Dim inputs As ArrayList = New ArrayList
            myInputs = inputs.ToArray(GetType(PlaylistModifierInput))
        End Sub

        Public Sub New(ByVal modifierFilePath As String, ByVal modifiersDirectory As String)
            InitializeModifierKeyLookup(modifiersDirectory)
            Load(modifierFilePath)
        End Sub


        Public Sub New(ByRef metaModifierComponent As XmlElement, ByVal modifiersDirectory As String)
            InitializeModifierKeyLookup(modifiersDirectory)
            Dim modifierKey As String = WebServiceClient.GetClient.XMLGetValueAt(metaModifierComponent, "ModifierKey", 0)
            Dim path As String = FindFilePathByModifierKey(modifierKey)

            If (File.Exists(path)) Then
                Dim modifierFile As New XmlDocument()
                modifierFile.Load(path)

                myDisplayName = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/DisplayName", 0)
                myAction = ConvertToAction(WebServiceClient.GetClient.XMLGetValueAt(metaModifierComponent, "./Action", 0))
                myModifierKey = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//PlaylistModifier/ModifierKey", 0)
                Dim index As Integer = 0
                Dim inputs As ArrayList = New ArrayList()

                Dim inputLabel As String = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/UILabel", index)
                Dim inputID As String = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/ID", index)
                Dim inputValue As String = WebServiceClient.GetClient.XMLGetValueAt(metaModifierComponent, "./Inputs/Input/Value", index)

                While (Not (inputLabel Is Nothing) And Not (inputID Is Nothing))
                    inputs.Add(New PlaylistModifierInput(inputLabel, inputID, inputValue))
                    index += 1
                    inputLabel = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/UILabel", index)
                    inputID = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/ID", index)
                    inputValue = WebServiceClient.GetClient.XMLGetValueAt(metaModifierComponent, "./Inputs/Input/Value", index)
                End While

                myFilePath = path
                myInputs = inputs.ToArray(GetType(PlaylistModifierInput))
                myType = TranslateModifierType(WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//PlaylistModifier/Type", 0))
            End If
        End Sub

        Public Sub New(ByVal toCopy As PlaylistModifierUILiason)
            myDisplayName = toCopy.DisplayName
            myAction = toCopy.myAction
            myFilePath = toCopy.myFilePath
            myModifierKey = toCopy.myModifierKey
            InitializeModifierKeyLookup(ModifiersDirectory)

            Dim inputsList As ArrayList = New ArrayList()

            For Each input As PlaylistModifierInput In toCopy.myInputs
                inputsList.Add(New PlaylistModifierInput(input.DisplayName, input.ID, input.Value))
            Next

            myInputs = inputsList.ToArray(GetType(PlaylistModifierInput))
            myType = toCopy.myType
        End Sub

        Private Shared Function FindFilePathByModifierKey(ByVal modifierKey As String) As String
            If (myModifierKeyLookup.ContainsKey(modifierKey)) Then
                Return myModifierKeyLookup.Item(modifierKey)
            End If

            Return Nothing
        End Function

        Private Shared Sub InitializeModifierKeyLookup(ByVal modifiersDirectory As String)
            If (myModifiersDirectory Is Nothing) Then
                myModifiersDirectory = modifiersDirectory

                If (Directory.Exists(modifiersDirectory)) Then
                    Dim modifierPaths As String() = Directory.GetFiles(modifiersDirectory, "*.xml", SearchOption.AllDirectories)
                    Dim modifierKeyXPath As String = "//PlaylistModifier/ModifierKey"

                    For Each path As String In modifierPaths
                        Dim modifierXML As New XmlDocument
                        modifierXML.Load(path)
                        Dim modifierKey As String = WebServiceClient.GetClient().XMLGetValueAt(modifierXML, modifierKeyXPath, 0)
                        If (Not myModifierKeyLookup.ContainsKey(modifierKey)) Then
                            myModifierKeyLookup.Add(modifierKey, path)
                        End If
                    Next

                End If
            End If
        End Sub



        Private Sub Load(ByRef modifierFilePath As String)
            If (File.Exists(modifierFilePath)) Then
                Dim modifierFile As XmlDocument = New XmlDocument()
                modifierFile.Load(modifierFilePath)
                myDisplayName = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/DisplayName", 0)
                myAction = ConvertToAction(WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//PlaylistModifier/Action", 0))
                myModifierKey = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//PlaylistModifier/ModifierKey", 0)
                Dim index As Integer = 0
                Dim inputs As ArrayList = New ArrayList()

                Dim inputLabel As String = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/UILabel", index)
                Dim inputID As String = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/ID", index)

                While (Not (inputLabel Is Nothing) And Not (inputID Is Nothing))
                    inputs.Add(New PlaylistModifierInput(inputLabel, inputID))
                    index += 1
                    inputLabel = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/UILabel", index)
                    inputID = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/ID", index)
                End While

                myFilePath = modifierFilePath
                myInputs = inputs.ToArray(GetType(PlaylistModifierInput))
                myType = TranslateModifierType(WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//PlaylistModifier/Type", 0))

            End If

        End Sub

        Public ReadOnly Property Type As ModifierType
            Get
                Return myType
            End Get
        End Property

        Private Function TranslateModifierType(ByVal typeName As String) As ModifierType
            If (typeName = "WMPAttribute") Then
                Return ModifierType.WMPAttribute
            ElseIf (typeName = "LastFM") Then
                Return ModifierType.LastFM
            ElseIf (typeName = "Meta") Then
                Return ModifierType.Meta
            End If

            Return Nothing
        End Function

        Private Function ConvertToAction(ByVal actionName As String) As IModifierAction
            If (actionName = "Add") Then
                Return New ModifierAction_Add
            ElseIf (actionName = "Subtract") Then
                Return New ModifierAction_Subtract
            ElseIf (actionName = "Filter") Then
                Return New ModifierAction_Filter
            End If

            Return Nothing
        End Function

        Public Property Inputs As PlaylistModifierInput()
            Get
                Return myInputs
            End Get
            Set(ByVal value As PlaylistModifierInput())
                myInputs = value
            End Set

        End Property

        Public Property FilePath As String
            Get
                Return myFilePath
            End Get
            Set(ByVal value As String)
                myFilePath = value
                myModifierKey = myFilePath
            End Set
        End Property

        Public Shared Function ModifiersDirectory() As String
            Return myModifiersDirectory
        End Function

        Public Property DisplayName As String
            Get
                Return myDisplayName
            End Get
            Set(ByVal value As String)
                myDisplayName = value
            End Set
        End Property


        Public Property ModifierAction As IModifierAction
            Get
                Return myAction
            End Get
            Set(ByVal value As IModifierAction)
                myAction = value
            End Set
        End Property



    End Class

End Class
