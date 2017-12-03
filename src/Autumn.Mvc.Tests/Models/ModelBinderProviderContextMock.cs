using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Tests.Models
{
    public class ModelBinderProviderContextMock : ModelBinderProviderContext
    {
        public ModelBinderProviderContextMock(Type type)
        {
            Metadata = new ModelMetadataMock(type);
        }
        
        public override IModelBinder CreateBinder(ModelMetadata metadata)
        {
           throw new System.NotImplementedException();
        }

        public override BindingInfo BindingInfo { get; }
        public override ModelMetadata Metadata { get; }
        public override IModelMetadataProvider MetadataProvider { get; }
     
    }
}