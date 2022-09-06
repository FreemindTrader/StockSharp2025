namespace StockSharp.Diagram
{
    /// <summary>
    /// An enumeration of the predefined ways in which models may be changed.
    /// </summary>
    public enum ModelChange
    {
        /// <summary>
        /// A transient data change that should not be considered a change to the model;
        /// such changes are ignored by the UndoManager and by the Diagram.
        /// </summary>
        None = -100, // 0xFFFFFF9C
        /// <summary>
        /// Cleared the contents of the UndoManager (for all models).
        /// </summary>
        ClearedUndoManager = -22, // 0xFFFFFFEA
        /// <summary>(for internal use)</summary>
        ReplacedReference = -21, // 0xFFFFFFEB
        /// <summary>
        /// Changed the shape or bounds of a node or one or more of its ports (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        InvalidateRelationships = -20, // 0xFFFFFFEC
        /// <summary>Finished a redo operation.</summary>
        FinishedRedo = -14, // 0xFFFFFFF2
        /// <summary>Finished an undo operation.</summary>
        FinishedUndo = -13, // 0xFFFFFFF3
        /// <summary>Starting a redo operation.</summary>
        StartingRedo = -5, // 0xFFFFFFFB
        /// <summary>Starting an undo operation.</summary>
        StartingUndo = -4, // 0xFFFFFFFC
        /// <summary>
        /// Rolled back the changes of a transaction and aborted it.
        /// </summary>
        RolledBackTransaction = -3, // 0xFFFFFFFD
        /// <summary>Committed the changes for a transaction.</summary>
        CommittedTransaction = -2, // 0xFFFFFFFE
        /// <summary>Started a transaction.</summary>
        StartedTransaction = -1, // 0xFFFFFFFF
        /// <summary>
        /// Changes to data properties and extended model properties, and other non-predefined property changes
        /// </summary>
        Property = 0,
        /// <summary>
        /// Changed (replaced) the NodesSource collection property (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        ChangedNodesSource = 1,
        /// <summary>
        /// Changed the NodeKeyPath property (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        ChangedNodeKeyPath = 2,
        /// <summary>
        /// Changed the NodeCategoryPath property (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        ChangedNodeCategoryPath = 3,
        /// <summary>
        /// Changed the NodeIsGroupPath property (GraphLinksModel, GraphModel)
        /// </summary>
        ChangedNodeIsGroupPath = 4,
        /// <summary>
        /// Changed the GroupNodePath property (GraphLinksModel, GraphModel)
        /// </summary>
        ChangedGroupNodePath = 5,
        /// <summary>
        /// Changed the MemberNodesPath property (GraphLinksModel, GraphModel)
        /// </summary>
        ChangedMemberNodesPath = 6,
        /// <summary>
        /// Changed the NodeIsLinkLabelPath property (GraphLinksModel)
        /// </summary>
        ChangedNodeIsLinkLabelPath = 7,
        /// <summary>
        /// Changed (replaced) the LinksSource collection property (GraphLinksModel)
        /// </summary>
        ChangedLinksSource = 8,
        /// <summary>Changed the LinkFromPath property (GraphLinksModel)</summary>
        ChangedLinkFromPath = 9,
        /// <summary>Changed the LinkToPath property (GraphLinksModel)</summary>
        ChangedLinkToPath = 10, // 0x0000000A
        /// <summary>Changed the FromNodesPath property (GraphModel)</summary>
        ChangedFromNodesPath = 11, // 0x0000000B
        /// <summary>Changed the ToNodesPath property (GraphModel)</summary>
        ChangedToNodesPath = 12, // 0x0000000C
        /// <summary>
        /// Changed the LinkLabelNodePath property (GraphLinksModel)
        /// </summary>
        ChangedLinkLabelNodePath = 13, // 0x0000000D
        /// <summary>
        /// Changed the LinkFromParameterPath property (GraphLinksModel)
        /// </summary>
        ChangedLinkFromParameterPath = 14, // 0x0000000E
        /// <summary>
        /// Changed the LinkToParameterPath property (GraphLinksModel)
        /// </summary>
        ChangedLinkToParameterPath = 15, // 0x0000000F
        /// <summary>
        /// Changed the LinkCategoryPath property (GraphLinksModel)
        /// </summary>
        ChangedLinkCategoryPath = 16, // 0x00000010
        /// <summary>
        /// Changed the Name property (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        ChangedName = 17, // 0x00000011
        /// <summary>
        /// Changed the DataFormat property (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        ChangedDataFormat = 18, // 0x00000012
        /// <summary>
        /// Changed the Modifiable property (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        ChangedModifiable = 19, // 0x00000013
        /// <summary>
        /// Changed the CopyingGroupCopiesMembers property (GraphLinksModel, GraphModel)
        /// </summary>
        ChangedCopyingGroupCopiesMembers = 20, // 0x00000014
        /// <summary>
        /// Changed the CopyingLinkCopiesLabel property (GraphLinksModel)
        /// </summary>
        ChangedCopyingLinkCopiesLabel = 21, // 0x00000015
        /// <summary>
        /// Changed the RemovingGroupRemovesMembers property (GraphLinksModel, GraphModel)
        /// </summary>
        ChangedRemovingGroupRemovesMembers = 22, // 0x00000016
        /// <summary>
        /// Changed the RemovingLinkRemovesLabel property (GraphLinksModel)
        /// </summary>
        ChangedRemovingLinkRemovesLabel = 23, // 0x00000017
        /// <summary>
        /// Changed the ValidCycle property (GraphLinksModel, GraphModel)
        /// </summary>
        ChangedValidCycle = 24, // 0x00000018
        /// <summary>
        /// Changed the ValidUnconnectedLinks property (GraphLinksModel)
        /// </summary>
        ChangedValidUnconnectedLinks = 25, // 0x00000019
        /// <summary>
        /// Added a node data to NodesSource (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        AddedNode = 26, // 0x0000001A
        /// <summary>
        /// About to remove a node data from NodesSource (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        RemovingNode = 27, // 0x0000001B
        /// <summary>
        /// Removed a node data from NodesSource (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        RemovedNode = 28, // 0x0000001C
        /// <summary>
        /// Changed the node key for a node data (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        ChangedNodeKey = 29, // 0x0000001D
        /// <summary>Added a link data to LinksSource (GraphLinksModel)</summary>
        AddedLink = 30, // 0x0000001E
        /// <summary>
        /// About to remove a link data from LinksSource (GraphLinksModel)
        /// </summary>
        RemovingLink = 31, // 0x0000001F
        /// <summary>
        /// Removed a link data from LinksSource (GraphLinksModel)
        /// </summary>
        RemovedLink = 32, // 0x00000020
        /// <summary>Changed the LinkFromPort property (GraphLinksModel)</summary>
        ChangedLinkFromPort = 33, // 0x00000021
        /// <summary>Changed the LinkToPort property (GraphLinksModel)</summary>
        ChangedLinkToPort = 34, // 0x00000022
        /// <summary>Changed the LinkLabelKey property (GraphLinksModel)</summary>
        ChangedLinkLabelKey = 35, // 0x00000023
        /// <summary>
        /// Changed (replaced) the FromNodeKeys collection property (GraphModel)
        /// </summary>
        ChangedFromNodeKeys = 36, // 0x00000024
        /// <summary>
        /// Added a node key to the FromNodeKeys collection property (GraphModel)
        /// </summary>
        AddedFromNodeKey = 37, // 0x00000025
        /// <summary>
        /// Removed a node key from the FromNodeKeys collection property (GraphModel)
        /// </summary>
        RemovedFromNodeKey = 38, // 0x00000026
        /// <summary>
        /// Changed (replaced) the ToNodeKeys collection property (GraphModel)
        /// </summary>
        ChangedToNodeKeys = 39, // 0x00000027
        /// <summary>
        /// Added a node key to the ToNodeKeys collection property (GraphModel)
        /// </summary>
        AddedToNodeKey = 40, // 0x00000028
        /// <summary>
        /// Removed a node key from the ToNodeKeys collection property (GraphModel)
        /// </summary>
        RemovedToNodeKey = 41, // 0x00000029
        /// <summary>
        /// Changed the GroupNodeKey property (GraphLinksModel, GraphModel)
        /// </summary>
        ChangedGroupNodeKey = 42, // 0x0000002A
        /// <summary>Changed the LinkGroupKey property (GraphLinksModel)</summary>
        ChangedLinkGroupNodeKey = 43, // 0x0000002B
        /// <summary>
        /// Changed (replaced) the MemberNodeKeys collection property (GraphLinksModel, GraphModel)
        /// </summary>
        ChangedMemberNodeKeys = 44, // 0x0000002C
        /// <summary>
        /// Added a node key to the MemberNodeKeys collection property (GraphLinksModel, GraphModel)
        /// </summary>
        AddedMemberNodeKey = 45, // 0x0000002D
        /// <summary>
        /// Removed a node key from the MemberNodeKeys collection property (GraphLinksModel, GraphModel)
        /// </summary>
        RemovedMemberNodeKey = 46, // 0x0000002E
        /// <summary>Changed the ParentNodeKey property (TreeModel)</summary>
        ChangedParentNodeKey = 47, // 0x0000002F
        /// <summary>
        /// Changed (replaced) the ChildNodeKeys collection property (TreeModel)
        /// </summary>
        ChangedChildNodeKeys = 48, // 0x00000030
        /// <summary>
        /// Added a node key to the ChildNodeKeys collection property (TreeModel)
        /// </summary>
        AddedChildNodeKey = 49, // 0x00000031
        /// <summary>
        /// Removed a node key from the ChildNodeKeys collection property (TreeModel)
        /// </summary>
        RemovedChildNodeKey = 50, // 0x00000032
        /// <summary>
        /// Changed the value of the Category for a node data (GraphLinksModel, GraphModel, TreeModel)
        /// </summary>
        ChangedNodeCategory = 51, // 0x00000033
        /// <summary>
        /// Changed the value of the Category for a link data (GraphLinksModel)
        /// </summary>
        ChangedLinkCategory = 52, // 0x00000034
    }
}
