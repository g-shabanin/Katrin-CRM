
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Katrin.Services.Metadata
{

using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    
public partial class CriteriaNode
{

	[DataMember]
    public System.Guid CriteriaNodeId { get; set; }

	[DataMember]
    public System.Guid CriteriaId { get; set; }

	[DataMember]
    public System.Guid ParentNodeId { get; set; }

	[DataMember]
    public string Operator { get; set; }

	[DataMember]
    public string OperatorType { get; set; }

	[DataMember]
    public Nullable<System.Guid> CompareAttributeId { get; set; }

	[DataMember]
    public string CompareValue { get; set; }



	[DataMember]
    public virtual Criterion Criterion { get; set; }

	[DataMember]
    public virtual EntityAttribute Metadata_Attribute { get; set; }

}

}