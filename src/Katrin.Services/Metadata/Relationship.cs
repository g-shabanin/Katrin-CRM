
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
    
public partial class Relationship
{

    public Relationship()
    {

        this.EntityRelationshipRelationships = new HashSet<EntityRelationshipRelationships>();

    }


	[DataMember]
    public System.Guid RelationshipId { get; set; }

	[DataMember]
    public string Name { get; set; }

	[DataMember]
    public System.Guid ReferencingEntityId { get; set; }

	[DataMember]
    public System.Guid ReferencingAttributeId { get; set; }

	[DataMember]
    public System.Guid ReferencedEntityId { get; set; }

	[DataMember]
    public System.Guid ReferencedAttributeId { get; set; }

	[DataMember]
    public Nullable<int> RelationshipType { get; set; }

	[DataMember]
    public byte CascadeDelete { get; set; }

	[DataMember]
    public byte[] VersionNumber { get; set; }



	[DataMember]
    public virtual EntityAttribute ReferencedAttribute { get; set; }

	[DataMember]
    public virtual EntityAttribute ReferencingAttribute { get; set; }

	[DataMember]
    public virtual Entity ReferencedEntity { get; set; }

	[DataMember]
    public virtual Entity ReferencingEntity { get; set; }

	[DataMember]
    public virtual ICollection<EntityRelationshipRelationships> EntityRelationshipRelationships { get; set; }

}

}
