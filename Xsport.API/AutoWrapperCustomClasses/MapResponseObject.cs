using AutoWrapper;

namespace Asup.Api.AutoWrapperCustomClasses
{
    public class MapResponseObject
    {
        [AutoWrapperPropertyMap(Prop.ResponseException)]
        public object? Error { get; set; }

        [AutoWrapperPropertyMap(Prop.ResponseException_ExceptionMessage)]
        public object? Message { get; set; }

        
        [AutoWrapperPropertyMap(Prop.Result)]
        public object? Data { get; set; }
        
    }
}