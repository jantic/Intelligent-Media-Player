Imports System.Xml
Imports System.Xml.XPath

Partial Public Class PlaylistManager


    Private Class URLTemplateMapping
        Private myTemplateURL As String
        Private myStringReplaceMappings As StringReplaceMapping()

        Public Sub New(ByRef liason As PlaylistModifierUILiason)
            myTemplateURL = RetrieveTemplateURL(liason)
            myStringReplaceMappings = StringReplaceMapping.LoadStringReplaceMappings(liason)
        End Sub

        Public Function GetResultingURL(ByRef inputs As PlaylistModifierInput())
            Dim resultURL As String = myTemplateURL

            For Each mapping As StringReplaceMapping In myStringReplaceMappings
                Dim toBeReplaced As String = mapping.Attribute
                Dim replacement As String = mapping.GetMatchingInputValue(inputs)
                resultURL = resultURL.Replace(toBeReplaced, replacement)
            Next

            Return resultURL
        End Function


        Function RetrieveTemplateURL(ByRef liason As PlaylistModifierUILiason) As String
            Dim modifierFile As XmlDocument = New XmlDocument()
            modifierFile.Load(liason.FilePath)
            Dim pathQuery As String = "//Method/LastFMrequest/RequestURL/TemplateURL"
            Dim value As String = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, pathQuery, 0)

            If (Not value Is Nothing) Then
                Return value
            End If

            Return Nothing
        End Function



        Private Class StringReplaceMapping
            Private myID As UInteger
            Private myAttribute As String
            Private myInputID As UInteger

            Private Sub New(ByVal theId As UInteger, ByVal theAttribute As String, ByVal theMapToID As UInteger)
                myID = theId
                myAttribute = theAttribute
                myInputID = theMapToID
            End Sub

            Public Shared Function LoadStringReplaceMappings(ByRef liason As PlaylistModifierUILiason) As StringReplaceMapping()
                Dim modifierFile As XmlDocument = New XmlDocument()
                modifierFile.Load(liason.FilePath)

                Dim mappings As ArrayList = New ArrayList
                Dim index As UInteger = 0
                Dim toReplace As String = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Method/LastFMrequest/RequestURL/TemplateMappings/Mapping/ToReplace", index)

                While (Not (toReplace Is Nothing))
                    Dim ID As UInteger = UInteger.Parse(WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Method/LastFMrequest/RequestURL/TemplateMappings/Mapping/ID", index))
                    Dim InputID = UInteger.Parse(WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Method/LastFMrequest/RequestURL/TemplateMappings/Mapping/InputID", index))

                    mappings.Add(New StringReplaceMapping(ID, toReplace, InputID))

                    index += 1
                    toReplace = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Method/LastFMrequest/RequestURL/TemplateMappings/Mapping/ToReplace", index)
                End While

                Return mappings.ToArray(GetType(StringReplaceMapping))
            End Function

            Public ReadOnly Property ID As UInteger
                Get
                    Return myID
                End Get
            End Property

            Public ReadOnly Property Attribute As String
                Get
                    Return myAttribute
                End Get
            End Property

            Public ReadOnly Property InputID As UInteger
                Get
                    Return myInputID
                End Get
            End Property

            Public Function GetMatchingInputValue(ByRef inputValues As PlaylistModifierInput()) As String
                For Each input As PlaylistModifierInput In inputValues
                    If (input.ID = myInputID) Then
                        Return input.Value
                    End If
                Next

                Return ""
            End Function


        End Class

    End Class

End Class

