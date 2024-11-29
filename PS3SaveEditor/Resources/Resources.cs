using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PS3SaveEditor.Resources
{
	// Token: 0x020001F5 RID: 501
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06001A8B RID: 6795 RVA: 0x00003ED8 File Offset: 0x000020D8
		internal Resources()
		{
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x000AD2B8 File Offset: 0x000AB4B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("PS3SaveEditor.Resources.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x000AD300 File Offset: 0x000AB500
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x000AD317 File Offset: 0x000AB517
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x000AD320 File Offset: 0x000AB520
		internal static Bitmap bg_company
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("bg_company", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x000AD350 File Offset: 0x000AB550
		internal static Bitmap blue
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("blue", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x000AD380 File Offset: 0x000AB580
		internal static string btnApply
		{
			get
			{
				return Resources.ResourceManager.GetString("btnApply", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x000AD3B0 File Offset: 0x000AB5B0
		internal static string btnApplyDownload
		{
			get
			{
				return Resources.ResourceManager.GetString("btnApplyDownload", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x000AD3E0 File Offset: 0x000AB5E0
		internal static string btnApplyPatch
		{
			get
			{
				return Resources.ResourceManager.GetString("btnApplyPatch", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x000AD410 File Offset: 0x000AB610
		internal static string btnBackup
		{
			get
			{
				return Resources.ResourceManager.GetString("btnBackup", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x000AD440 File Offset: 0x000AB640
		internal static string btnBrowse
		{
			get
			{
				return Resources.ResourceManager.GetString("btnBrowse", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x000AD470 File Offset: 0x000AB670
		internal static string btnCancel
		{
			get
			{
				return Resources.ResourceManager.GetString("btnCancel", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x000AD4A0 File Offset: 0x000AB6A0
		internal static string btnCancellation
		{
			get
			{
				return Resources.ResourceManager.GetString("btnCancellation", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x000AD4D0 File Offset: 0x000AB6D0
		internal static string btnCheats
		{
			get
			{
				return Resources.ResourceManager.GetString("btnCheats", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x000AD500 File Offset: 0x000AB700
		internal static string btnClose
		{
			get
			{
				return Resources.ResourceManager.GetString("btnClose", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x000AD530 File Offset: 0x000AB730
		internal static string btnCompare
		{
			get
			{
				return Resources.ResourceManager.GetString("btnCompare", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x000AD560 File Offset: 0x000AB760
		internal static string btnDeactivate
		{
			get
			{
				return Resources.ResourceManager.GetString("btnDeactivate", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x000AD590 File Offset: 0x000AB790
		internal static string btnDiagnostic
		{
			get
			{
				return Resources.ResourceManager.GetString("btnDiagnostic", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x000AD5C0 File Offset: 0x000AB7C0
		internal static string btnFind
		{
			get
			{
				return Resources.ResourceManager.GetString("btnFind", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x000AD5F0 File Offset: 0x000AB7F0
		internal static string btnFindPrev
		{
			get
			{
				return Resources.ResourceManager.GetString("btnFindPrev", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x000AD620 File Offset: 0x000AB820
		internal static string btnHelp
		{
			get
			{
				return Resources.ResourceManager.GetString("btnHelp", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x000AD650 File Offset: 0x000AB850
		internal static string btnHome
		{
			get
			{
				return Resources.ResourceManager.GetString("btnHome", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x000AD680 File Offset: 0x000AB880
		internal static string btnImport
		{
			get
			{
				return Resources.ResourceManager.GetString("btnImport", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x000AD6B0 File Offset: 0x000AB8B0
		internal static string btnManageProfiles
		{
			get
			{
				return Resources.ResourceManager.GetString("btnManageProfiles", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x000AD6E0 File Offset: 0x000AB8E0
		internal static string btnNew
		{
			get
			{
				return Resources.ResourceManager.GetString("btnNew", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x000AD710 File Offset: 0x000AB910
		internal static string btnOK
		{
			get
			{
				return Resources.ResourceManager.GetString("btnOK", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x000AD740 File Offset: 0x000AB940
		internal static string btnOpenFolder
		{
			get
			{
				return Resources.ResourceManager.GetString("btnOpenFolder", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x000AD770 File Offset: 0x000AB970
		internal static string btnOptions
		{
			get
			{
				return Resources.ResourceManager.GetString("btnOptions", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x000AD7A0 File Offset: 0x000AB9A0
		internal static string btnPage1
		{
			get
			{
				return Resources.ResourceManager.GetString("btnPage1", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x000AD7D0 File Offset: 0x000AB9D0
		internal static string btnPage2
		{
			get
			{
				return Resources.ResourceManager.GetString("btnPage2", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000AD800 File Offset: 0x000ABA00
		internal static string btnPop
		{
			get
			{
				return Resources.ResourceManager.GetString("btnPop", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x000AD830 File Offset: 0x000ABA30
		internal static string btnPush
		{
			get
			{
				return Resources.ResourceManager.GetString("btnPush", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x000AD860 File Offset: 0x000ABA60
		internal static string btnResign
		{
			get
			{
				return Resources.ResourceManager.GetString("btnResign", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x000AD890 File Offset: 0x000ABA90
		internal static string btnRestore
		{
			get
			{
				return Resources.ResourceManager.GetString("btnRestore", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x000AD8C0 File Offset: 0x000ABAC0
		internal static string btnRss
		{
			get
			{
				return Resources.ResourceManager.GetString("btnRss", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x000AD8F0 File Offset: 0x000ABAF0
		internal static string btnSave
		{
			get
			{
				return Resources.ResourceManager.GetString("btnSave", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x000AD920 File Offset: 0x000ABB20
		internal static string btnSaveCodes
		{
			get
			{
				return Resources.ResourceManager.GetString("btnSaveCodes", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x000AD950 File Offset: 0x000ABB50
		internal static string btnSaves
		{
			get
			{
				return Resources.ResourceManager.GetString("btnSaves", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x000AD980 File Offset: 0x000ABB80
		internal static string btnStack
		{
			get
			{
				return Resources.ResourceManager.GetString("btnStack", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x000AD9B0 File Offset: 0x000ABBB0
		internal static string btnUpdate
		{
			get
			{
				return Resources.ResourceManager.GetString("btnUpdate", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x000AD9E0 File Offset: 0x000ABBE0
		internal static string btnUserAccount
		{
			get
			{
				return Resources.ResourceManager.GetString("btnUserAccount", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x000ADA10 File Offset: 0x000ABC10
		internal static string btnViewAllCheats
		{
			get
			{
				return Resources.ResourceManager.GetString("btnViewAllCheats", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x000ADA40 File Offset: 0x000ABC40
		internal static Bitmap check
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("check", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x000ADA70 File Offset: 0x000ABC70
		internal static string chkBackupSaves
		{
			get
			{
				return Resources.ResourceManager.GetString("chkBackupSaves", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x000ADAA0 File Offset: 0x000ABCA0
		internal static string chkDeleteExisting
		{
			get
			{
				return Resources.ResourceManager.GetString("chkDeleteExisting", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x000ADAD0 File Offset: 0x000ABCD0
		internal static string chkDontShowResign
		{
			get
			{
				return Resources.ResourceManager.GetString("chkDontShowResign", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x000ADB00 File Offset: 0x000ABD00
		internal static string chkEnableRight
		{
			get
			{
				return Resources.ResourceManager.GetString("chkEnableRight", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x000ADB30 File Offset: 0x000ABD30
		internal static string chkShowAll
		{
			get
			{
				return Resources.ResourceManager.GetString("chkShowAll", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x000ADB60 File Offset: 0x000ABD60
		internal static string chkSyncScroll
		{
			get
			{
				return Resources.ResourceManager.GetString("chkSyncScroll", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x000ADB90 File Offset: 0x000ABD90
		internal static string colAdded
		{
			get
			{
				return Resources.ResourceManager.GetString("colAdded", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x000ADBC0 File Offset: 0x000ABDC0
		internal static string colBytes
		{
			get
			{
				return Resources.ResourceManager.GetString("colBytes", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x000ADBF0 File Offset: 0x000ABDF0
		internal static string colCheats
		{
			get
			{
				return Resources.ResourceManager.GetString("colCheats", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x000ADC20 File Offset: 0x000ABE20
		internal static string colComment
		{
			get
			{
				return Resources.ResourceManager.GetString("colComment", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x000ADC50 File Offset: 0x000ABE50
		internal static string colDefault
		{
			get
			{
				return Resources.ResourceManager.GetString("colDefault", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x000ADC80 File Offset: 0x000ABE80
		internal static string colDesc
		{
			get
			{
				return Resources.ResourceManager.GetString("colDesc", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x000ADCB0 File Offset: 0x000ABEB0
		internal static string colDiskId
		{
			get
			{
				return Resources.ResourceManager.GetString("colDiskId", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x000ADCE0 File Offset: 0x000ABEE0
		internal static string colEndAddr
		{
			get
			{
				return Resources.ResourceManager.GetString("colEndAddr", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x000ADD10 File Offset: 0x000ABF10
		internal static string colGameCode
		{
			get
			{
				return Resources.ResourceManager.GetString("colGameCode", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x000ADD40 File Offset: 0x000ABF40
		internal static string colGameName
		{
			get
			{
				return Resources.ResourceManager.GetString("colGameName", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x000ADD70 File Offset: 0x000ABF70
		internal static string colName
		{
			get
			{
				return Resources.ResourceManager.GetString("colName", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x000ADDA0 File Offset: 0x000ABFA0
		internal static string colProfile
		{
			get
			{
				return Resources.ResourceManager.GetString("colProfile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x000ADDD0 File Offset: 0x000ABFD0
		internal static string colProfileName
		{
			get
			{
				return Resources.ResourceManager.GetString("colProfileName", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x000ADE00 File Offset: 0x000AC000
		internal static string colRegion
		{
			get
			{
				return Resources.ResourceManager.GetString("colRegion", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x000ADE30 File Offset: 0x000AC030
		internal static string colSelect
		{
			get
			{
				return Resources.ResourceManager.GetString("colSelect", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x000ADE60 File Offset: 0x000AC060
		internal static string colStartAddr
		{
			get
			{
				return Resources.ResourceManager.GetString("colStartAddr", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x000ADE90 File Offset: 0x000AC090
		internal static string colSysVer
		{
			get
			{
				return Resources.ResourceManager.GetString("colSysVer", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000ADEC0 File Offset: 0x000AC0C0
		internal static string colUpdated
		{
			get
			{
				return Resources.ResourceManager.GetString("colUpdated", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x000ADEF0 File Offset: 0x000AC0F0
		internal static string colVersion
		{
			get
			{
				return Resources.ResourceManager.GetString("colVersion", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x000ADF20 File Offset: 0x000AC120
		internal static Bitmap company
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("company", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x000ADF50 File Offset: 0x000AC150
		internal static string descResign
		{
			get
			{
				return Resources.ResourceManager.GetString("descResign", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x000ADF80 File Offset: 0x000AC180
		internal static string doNotUseRemovable
		{
			get
			{
				return Resources.ResourceManager.GetString("doNotUseRemovable", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x000ADFB0 File Offset: 0x000AC1B0
		internal static Icon dp
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("dp", Resources.resourceCulture);
				return (Icon)@object;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x000ADFE0 File Offset: 0x000AC1E0
		internal static string err10001
		{
			get
			{
				return Resources.ResourceManager.GetString("err10001", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x000AE010 File Offset: 0x000AC210
		internal static string err10002
		{
			get
			{
				return Resources.ResourceManager.GetString("err10002", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x000AE040 File Offset: 0x000AC240
		internal static string err10003
		{
			get
			{
				return Resources.ResourceManager.GetString("err10003", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x000AE070 File Offset: 0x000AC270
		internal static string err10004
		{
			get
			{
				return Resources.ResourceManager.GetString("err10004", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x000AE0A0 File Offset: 0x000AC2A0
		internal static string err10005
		{
			get
			{
				return Resources.ResourceManager.GetString("err10005", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x000AE0D0 File Offset: 0x000AC2D0
		internal static string err10006
		{
			get
			{
				return Resources.ResourceManager.GetString("err10006", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x000AE100 File Offset: 0x000AC300
		internal static string err10007
		{
			get
			{
				return Resources.ResourceManager.GetString("err10007", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x000AE130 File Offset: 0x000AC330
		internal static string err10008
		{
			get
			{
				return Resources.ResourceManager.GetString("err10008", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x000AE160 File Offset: 0x000AC360
		internal static string err10009
		{
			get
			{
				return Resources.ResourceManager.GetString("err10009", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x000AE190 File Offset: 0x000AC390
		internal static string err10010
		{
			get
			{
				return Resources.ResourceManager.GetString("err10010", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x000AE1C0 File Offset: 0x000AC3C0
		internal static string err10011
		{
			get
			{
				return Resources.ResourceManager.GetString("err10011", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x000AE1F0 File Offset: 0x000AC3F0
		internal static string err10012
		{
			get
			{
				return Resources.ResourceManager.GetString("err10012", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000AE220 File Offset: 0x000AC420
		internal static string err10013
		{
			get
			{
				return Resources.ResourceManager.GetString("err10013", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x000AE250 File Offset: 0x000AC450
		internal static string err10014
		{
			get
			{
				return Resources.ResourceManager.GetString("err10014", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x000AE280 File Offset: 0x000AC480
		internal static string err10015
		{
			get
			{
				return Resources.ResourceManager.GetString("err10015", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x000AE2B0 File Offset: 0x000AC4B0
		internal static string err10016
		{
			get
			{
				return Resources.ResourceManager.GetString("err10016", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x000AE2E0 File Offset: 0x000AC4E0
		internal static string err10017
		{
			get
			{
				return Resources.ResourceManager.GetString("err10017", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x000AE310 File Offset: 0x000AC510
		internal static string err10018
		{
			get
			{
				return Resources.ResourceManager.GetString("err10018", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x000AE340 File Offset: 0x000AC540
		internal static string err10019
		{
			get
			{
				return Resources.ResourceManager.GetString("err10019", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000AE370 File Offset: 0x000AC570
		internal static string err10020
		{
			get
			{
				return Resources.ResourceManager.GetString("err10020", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x000AE3A0 File Offset: 0x000AC5A0
		internal static string err10021
		{
			get
			{
				return Resources.ResourceManager.GetString("err10021", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000AE3D0 File Offset: 0x000AC5D0
		internal static string err10022
		{
			get
			{
				return Resources.ResourceManager.GetString("err10022", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x000AE400 File Offset: 0x000AC600
		internal static string err10023
		{
			get
			{
				return Resources.ResourceManager.GetString("err10023", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x000AE430 File Offset: 0x000AC630
		internal static string err4000
		{
			get
			{
				return Resources.ResourceManager.GetString("err4000", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x000AE460 File Offset: 0x000AC660
		internal static string err4005
		{
			get
			{
				return Resources.ResourceManager.GetString("err4005", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x000AE490 File Offset: 0x000AC690
		internal static string err4010
		{
			get
			{
				return Resources.ResourceManager.GetString("err4010", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x000AE4C0 File Offset: 0x000AC6C0
		internal static string err4015
		{
			get
			{
				return Resources.ResourceManager.GetString("err4015", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x000AE4F0 File Offset: 0x000AC6F0
		internal static string err4020
		{
			get
			{
				return Resources.ResourceManager.GetString("err4020", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000AE520 File Offset: 0x000AC720
		internal static string err4021
		{
			get
			{
				return Resources.ResourceManager.GetString("err4021", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x000AE550 File Offset: 0x000AC750
		internal static string err4022
		{
			get
			{
				return Resources.ResourceManager.GetString("err4022", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000AE580 File Offset: 0x000AC780
		internal static string err4025
		{
			get
			{
				return Resources.ResourceManager.GetString("err4025", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x000AE5B0 File Offset: 0x000AC7B0
		internal static string err4030
		{
			get
			{
				return Resources.ResourceManager.GetString("err4030", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000AE5E0 File Offset: 0x000AC7E0
		internal static string err4035
		{
			get
			{
				return Resources.ResourceManager.GetString("err4035", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x000AE610 File Offset: 0x000AC810
		internal static string err4040
		{
			get
			{
				return Resources.ResourceManager.GetString("err4040", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x000AE640 File Offset: 0x000AC840
		internal static string err4041
		{
			get
			{
				return Resources.ResourceManager.GetString("err4041", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000AE670 File Offset: 0x000AC870
		internal static string err4042
		{
			get
			{
				return Resources.ResourceManager.GetString("err4042", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x000AE6A0 File Offset: 0x000AC8A0
		internal static string err4043
		{
			get
			{
				return Resources.ResourceManager.GetString("err4043", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x000AE6D0 File Offset: 0x000AC8D0
		internal static string err4045
		{
			get
			{
				return Resources.ResourceManager.GetString("err4045", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x000AE700 File Offset: 0x000AC900
		internal static string err4050
		{
			get
			{
				return Resources.ResourceManager.GetString("err4050", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x000AE730 File Offset: 0x000AC930
		internal static string err4055
		{
			get
			{
				return Resources.ResourceManager.GetString("err4055", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x000AE760 File Offset: 0x000AC960
		internal static string err4060
		{
			get
			{
				return Resources.ResourceManager.GetString("err4060", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x000AE790 File Offset: 0x000AC990
		internal static string err4065
		{
			get
			{
				return Resources.ResourceManager.GetString("err4065", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x000AE7C0 File Offset: 0x000AC9C0
		internal static string err4066
		{
			get
			{
				return Resources.ResourceManager.GetString("err4066", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x000AE7F0 File Offset: 0x000AC9F0
		internal static string err4069
		{
			get
			{
				return Resources.ResourceManager.GetString("err4069", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x000AE820 File Offset: 0x000ACA20
		internal static string err4070
		{
			get
			{
				return Resources.ResourceManager.GetString("err4070", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x000AE850 File Offset: 0x000ACA50
		internal static string err4071
		{
			get
			{
				return Resources.ResourceManager.GetString("err4071", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x000AE880 File Offset: 0x000ACA80
		internal static string err4090
		{
			get
			{
				return Resources.ResourceManager.GetString("err4090", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x000AE8B0 File Offset: 0x000ACAB0
		internal static string err4095
		{
			get
			{
				return Resources.ResourceManager.GetString("err4095", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x000AE8E0 File Offset: 0x000ACAE0
		internal static string err4100
		{
			get
			{
				return Resources.ResourceManager.GetString("err4100", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x000AE910 File Offset: 0x000ACB10
		internal static string err4101
		{
			get
			{
				return Resources.ResourceManager.GetString("err4101", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x000AE940 File Offset: 0x000ACB40
		internal static string err4105
		{
			get
			{
				return Resources.ResourceManager.GetString("err4105", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x000AE970 File Offset: 0x000ACB70
		internal static string err4110
		{
			get
			{
				return Resources.ResourceManager.GetString("err4110", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x000AE9A0 File Offset: 0x000ACBA0
		internal static string err4115
		{
			get
			{
				return Resources.ResourceManager.GetString("err4115", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x000AE9D0 File Offset: 0x000ACBD0
		internal static string err4116
		{
			get
			{
				return Resources.ResourceManager.GetString("err4116", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x000AEA00 File Offset: 0x000ACC00
		internal static string err4120
		{
			get
			{
				return Resources.ResourceManager.GetString("err4120", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x000AEA30 File Offset: 0x000ACC30
		internal static string err4121
		{
			get
			{
				return Resources.ResourceManager.GetString("err4121", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000AEA60 File Offset: 0x000ACC60
		internal static string err4122
		{
			get
			{
				return Resources.ResourceManager.GetString("err4122", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000AEA90 File Offset: 0x000ACC90
		internal static string err4124
		{
			get
			{
				return Resources.ResourceManager.GetString("err4124", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000AEAC0 File Offset: 0x000ACCC0
		internal static string err4500
		{
			get
			{
				return Resources.ResourceManager.GetString("err4500", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000AEAF0 File Offset: 0x000ACCF0
		internal static string err7498
		{
			get
			{
				return Resources.ResourceManager.GetString("err7498", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000AEB20 File Offset: 0x000ACD20
		internal static string errAnotherInstance
		{
			get
			{
				return Resources.ResourceManager.GetString("errAnotherInstance", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x000AEB50 File Offset: 0x000ACD50
		internal static string errCheatExists
		{
			get
			{
				return Resources.ResourceManager.GetString("errCheatExists", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x000AEB80 File Offset: 0x000ACD80
		internal static string errConnection
		{
			get
			{
				return Resources.ResourceManager.GetString("errConnection", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x000AEBB0 File Offset: 0x000ACDB0
		internal static string errContactSupport
		{
			get
			{
				return Resources.ResourceManager.GetString("errContactSupport", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x000AEBE0 File Offset: 0x000ACDE0
		internal static string errDelete
		{
			get
			{
				return Resources.ResourceManager.GetString("errDelete", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x000AEC10 File Offset: 0x000ACE10
		internal static string errDuplicateProfile
		{
			get
			{
				return Resources.ResourceManager.GetString("errDuplicateProfile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x000AEC40 File Offset: 0x000ACE40
		internal static string errExtract
		{
			get
			{
				return Resources.ResourceManager.GetString("errExtract", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x000AEC70 File Offset: 0x000ACE70
		internal static string errFileSizeOutOfRange
		{
			get
			{
				return Resources.ResourceManager.GetString("errFileSizeOutOfRange", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x000AECA0 File Offset: 0x000ACEA0
		internal static string errImportNoUSB
		{
			get
			{
				return Resources.ResourceManager.GetString("errImportNoUSB", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x000AECD0 File Offset: 0x000ACED0
		internal static string errIncorrectValue
		{
			get
			{
				return Resources.ResourceManager.GetString("errIncorrectValue", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x000AED00 File Offset: 0x000ACF00
		internal static string errInternal
		{
			get
			{
				return Resources.ResourceManager.GetString("errInternal", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x000AED30 File Offset: 0x000ACF30
		internal static string errInvalidAddress
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidAddress", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x000AED60 File Offset: 0x000ACF60
		internal static string errInvalidCode
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidCode", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x000AED90 File Offset: 0x000ACF90
		internal static string errInvalidDesc
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidDesc", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x000AEDC0 File Offset: 0x000ACFC0
		internal static string errInvalidFCode
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidFCode", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x000AEDF0 File Offset: 0x000ACFF0
		internal static string errInvalidHex
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidHex", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x000AEE20 File Offset: 0x000AD020
		internal static string errInvalidHexCode
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidHexCode", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x000AEE50 File Offset: 0x000AD050
		internal static string errInvalidResponse
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidResponse", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x000AEE80 File Offset: 0x000AD080
		internal static string errInvalidSave
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidSave", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x000AEEB0 File Offset: 0x000AD0B0
		internal static string errInvalidSerial
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidSerial", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x000AEEE0 File Offset: 0x000AD0E0
		internal static string errInvalidUSB
		{
			get
			{
				return Resources.ResourceManager.GetString("errInvalidUSB", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x000AEF10 File Offset: 0x000AD110
		internal static string errMaxCodes
		{
			get
			{
				return Resources.ResourceManager.GetString("errMaxCodes", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x000AEF40 File Offset: 0x000AD140
		internal static string errMaxProfiles
		{
			get
			{
				return Resources.ResourceManager.GetString("errMaxProfiles", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x000AEF70 File Offset: 0x000AD170
		internal static string errNoBackup
		{
			get
			{
				return Resources.ResourceManager.GetString("errNoBackup", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x000AEFA0 File Offset: 0x000AD1A0
		internal static string errNoDefault
		{
			get
			{
				return Resources.ResourceManager.GetString("errNoDefault", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x000AEFD0 File Offset: 0x000AD1D0
		internal static string errNoFile
		{
			get
			{
				return Resources.ResourceManager.GetString("errNoFile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x000AF000 File Offset: 0x000AD200
		internal static string errNoSavedata
		{
			get
			{
				return Resources.ResourceManager.GetString("errNoSavedata", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x000AF030 File Offset: 0x000AD230
		internal static string errNotRegistered
		{
			get
			{
				return Resources.ResourceManager.GetString("errNotRegistered", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x000AF060 File Offset: 0x000AD260
		internal static string errOffline
		{
			get
			{
				return Resources.ResourceManager.GetString("errOffline", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x000AF090 File Offset: 0x000AD290
		internal static string errOneCheat
		{
			get
			{
				return Resources.ResourceManager.GetString("errOneCheat", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x000AF0C0 File Offset: 0x000AD2C0
		internal static string errProfileExist
		{
			get
			{
				return Resources.ResourceManager.GetString("errProfileExist", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x000AF0F0 File Offset: 0x000AD2F0
		internal static string errProfileLock
		{
			get
			{
				return Resources.ResourceManager.GetString("errProfileLock", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x000AF120 File Offset: 0x000AD320
		internal static string errPSNNameUsed
		{
			get
			{
				return Resources.ResourceManager.GetString("errPSNNameUsed", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x000AF150 File Offset: 0x000AD350
		internal static string errResignNoUSB
		{
			get
			{
				return Resources.ResourceManager.GetString("errResignNoUSB", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x000AF180 File Offset: 0x000AD380
		internal static string errSaveData
		{
			get
			{
				return Resources.ResourceManager.GetString("errSaveData", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x000AF1B0 File Offset: 0x000AD3B0
		internal static string errSerial
		{
			get
			{
				return Resources.ResourceManager.GetString("errSerial", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x000AF1E0 File Offset: 0x000AD3E0
		internal static string errServer
		{
			get
			{
				return Resources.ResourceManager.GetString("errServer", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000AF210 File Offset: 0x000AD410
		internal static string errServerConnection
		{
			get
			{
				return Resources.ResourceManager.GetString("errServerConnection", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x000AF240 File Offset: 0x000AD440
		internal static string errTooManyTimes
		{
			get
			{
				return Resources.ResourceManager.GetString("errTooManyTimes", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x000AF270 File Offset: 0x000AD470
		internal static string errUnknown
		{
			get
			{
				return Resources.ResourceManager.GetString("errUnknown", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x000AF2A0 File Offset: 0x000AD4A0
		internal static string errUpgrade
		{
			get
			{
				return Resources.ResourceManager.GetString("errUpgrade", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x000AF2D0 File Offset: 0x000AD4D0
		internal static string errWriteForbidden
		{
			get
			{
				return Resources.ResourceManager.GetString("errWriteForbidden", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x000AF300 File Offset: 0x000AD500
		internal static string gamelistDownloaderMsg
		{
			get
			{
				return Resources.ResourceManager.GetString("gamelistDownloaderMsg", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x000AF330 File Offset: 0x000AD530
		internal static string gamelistDownloaderTitle
		{
			get
			{
				return Resources.ResourceManager.GetString("gamelistDownloaderTitle", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x000AF360 File Offset: 0x000AD560
		internal static string gbBackupLocation
		{
			get
			{
				return Resources.ResourceManager.GetString("gbBackupLocation", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x000AF390 File Offset: 0x000AD590
		internal static Bitmap home_gamelist_off
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("home_gamelist_off", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x000AF3C0 File Offset: 0x000AD5C0
		internal static Bitmap home_gamelist_on
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("home_gamelist_on", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x000AF3F0 File Offset: 0x000AD5F0
		internal static Bitmap home_help_off
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("home_help_off", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x000AF420 File Offset: 0x000AD620
		internal static Bitmap home_help_on
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("home_help_on", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x000AF450 File Offset: 0x000AD650
		internal static Bitmap home_settings_off
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("home_settings_off", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x000AF480 File Offset: 0x000AD680
		internal static Bitmap home_settings_on
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("home_settings_on", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x000AF4B0 File Offset: 0x000AD6B0
		internal static string itmDec
		{
			get
			{
				return Resources.ResourceManager.GetString("itmDec", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x000AF4E0 File Offset: 0x000AD6E0
		internal static string itmHex
		{
			get
			{
				return Resources.ResourceManager.GetString("itmHex", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x000AF510 File Offset: 0x000AD710
		internal static string lblAddressExtra
		{
			get
			{
				return Resources.ResourceManager.GetString("lblAddressExtra", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x000AF540 File Offset: 0x000AD740
		internal static string lblAvailableGames
		{
			get
			{
				return Resources.ResourceManager.GetString("lblAvailableGames", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x000AF570 File Offset: 0x000AD770
		internal static string lblBackingUp
		{
			get
			{
				return Resources.ResourceManager.GetString("lblBackingUp", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x000AF5A0 File Offset: 0x000AD7A0
		internal static string lblCheatCodes
		{
			get
			{
				return Resources.ResourceManager.GetString("lblCheatCodes", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x000AF5D0 File Offset: 0x000AD7D0
		internal static string lblCheats
		{
			get
			{
				return Resources.ResourceManager.GetString("lblCheats", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x000AF600 File Offset: 0x000AD800
		internal static string lblCheckSession
		{
			get
			{
				return Resources.ResourceManager.GetString("lblCheckSession", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x000AF630 File Offset: 0x000AD830
		internal static string lblCheckVersion
		{
			get
			{
				return Resources.ResourceManager.GetString("lblCheckVersion", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x000AF660 File Offset: 0x000AD860
		internal static string lblCodes
		{
			get
			{
				return Resources.ResourceManager.GetString("lblCodes", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x000AF690 File Offset: 0x000AD890
		internal static string lblComment
		{
			get
			{
				return Resources.ResourceManager.GetString("lblComment", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x000AF6C0 File Offset: 0x000AD8C0
		internal static string lblCompressing
		{
			get
			{
				return Resources.ResourceManager.GetString("lblCompressing", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x000AF6F0 File Offset: 0x000AD8F0
		internal static string lblCurrentFile
		{
			get
			{
				return Resources.ResourceManager.GetString("lblCurrentFile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x000AF720 File Offset: 0x000AD920
		internal static string lblDataAscii
		{
			get
			{
				return Resources.ResourceManager.GetString("lblDataAscii", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000AF750 File Offset: 0x000AD950
		internal static string lblDataHex
		{
			get
			{
				return Resources.ResourceManager.GetString("lblDataHex", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000AF780 File Offset: 0x000AD980
		internal static string lblDeactivate
		{
			get
			{
				return Resources.ResourceManager.GetString("lblDeactivate", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x000AF7B0 File Offset: 0x000AD9B0
		internal static string lblDeleteProfile
		{
			get
			{
				return Resources.ResourceManager.GetString("lblDeleteProfile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x000AF7E0 File Offset: 0x000AD9E0
		internal static string lblDescription
		{
			get
			{
				return Resources.ResourceManager.GetString("lblDescription", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x000AF810 File Offset: 0x000ADA10
		internal static string lblDiagnosticSection
		{
			get
			{
				return Resources.ResourceManager.GetString("lblDiagnosticSection", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x000AF840 File Offset: 0x000ADA40
		internal static string lblDownloadStatus
		{
			get
			{
				return Resources.ResourceManager.GetString("lblDownloadStatus", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x000AF870 File Offset: 0x000ADA70
		internal static string lblDrive
		{
			get
			{
				return Resources.ResourceManager.GetString("lblDrive", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x000AF8A0 File Offset: 0x000ADAA0
		internal static string lblEnterLoc
		{
			get
			{
				return Resources.ResourceManager.GetString("lblEnterLoc", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x000AF8D0 File Offset: 0x000ADAD0
		internal static string lblEnterSerial
		{
			get
			{
				return Resources.ResourceManager.GetString("lblEnterSerial", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x000AF900 File Offset: 0x000ADB00
		internal static string lblInstruction
		{
			get
			{
				return Resources.ResourceManager.GetString("lblInstruction", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x000AF930 File Offset: 0x000ADB30
		internal static string lblInstruction_2
		{
			get
			{
				return Resources.ResourceManager.GetString("lblInstruction_2", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x000AF960 File Offset: 0x000ADB60
		internal static string lblInstruction1
		{
			get
			{
				return Resources.ResourceManager.GetString("lblInstruction1", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000AF990 File Offset: 0x000ADB90
		internal static string lblInstruction1Red
		{
			get
			{
				return Resources.ResourceManager.GetString("lblInstruction1Red", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x000AF9C0 File Offset: 0x000ADBC0
		internal static string lblInstruction2
		{
			get
			{
				return Resources.ResourceManager.GetString("lblInstruction2", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x000AF9F0 File Offset: 0x000ADBF0
		internal static string lblInstruction3
		{
			get
			{
				return Resources.ResourceManager.GetString("lblInstruction3", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x000AFA20 File Offset: 0x000ADC20
		internal static string lblInstructionPage2
		{
			get
			{
				return Resources.ResourceManager.GetString("lblInstructionPage2", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000AFA50 File Offset: 0x000ADC50
		internal static string lblInstructionsPage1
		{
			get
			{
				return Resources.ResourceManager.GetString("lblInstructionsPage1", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x000AFA80 File Offset: 0x000ADC80
		internal static string lblInstuctionPage3
		{
			get
			{
				return Resources.ResourceManager.GetString("lblInstuctionPage3", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x000AFAB0 File Offset: 0x000ADCB0
		internal static string lblLanguage
		{
			get
			{
				return Resources.ResourceManager.GetString("lblLanguage", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x000AFAE0 File Offset: 0x000ADCE0
		internal static string lblLength
		{
			get
			{
				return Resources.ResourceManager.GetString("lblLength", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x000AFB10 File Offset: 0x000ADD10
		internal static string lblLicHelp
		{
			get
			{
				return Resources.ResourceManager.GetString("lblLicHelp", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x000AFB40 File Offset: 0x000ADD40
		internal static string lblManageProfiles
		{
			get
			{
				return Resources.ResourceManager.GetString("lblManageProfiles", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x000AFB70 File Offset: 0x000ADD70
		internal static string lblNoCheats
		{
			get
			{
				return Resources.ResourceManager.GetString("lblNoCheats", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x000AFBA0 File Offset: 0x000ADDA0
		internal static string lblNoSaves
		{
			get
			{
				return Resources.ResourceManager.GetString("lblNoSaves", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000AFBD0 File Offset: 0x000ADDD0
		internal static string lblOffset
		{
			get
			{
				return Resources.ResourceManager.GetString("lblOffset", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x000AFC00 File Offset: 0x000ADE00
		internal static string lblPackageSerial
		{
			get
			{
				return Resources.ResourceManager.GetString("lblPackageSerial", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x000AFC30 File Offset: 0x000ADE30
		internal static string lblPage1
		{
			get
			{
				return Resources.ResourceManager.GetString("lblPage1", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000AFC60 File Offset: 0x000ADE60
		internal static string lblPage2
		{
			get
			{
				return Resources.ResourceManager.GetString("lblPage2", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x000AFC90 File Offset: 0x000ADE90
		internal static string lblPage21
		{
			get
			{
				return Resources.ResourceManager.GetString("lblPage21", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x000AFCC0 File Offset: 0x000ADEC0
		internal static string lblPreparing
		{
			get
			{
				return Resources.ResourceManager.GetString("lblPreparing", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x000AFCF0 File Offset: 0x000ADEF0
		internal static string lblProfile
		{
			get
			{
				return Resources.ResourceManager.GetString("lblProfile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x000AFD20 File Offset: 0x000ADF20
		internal static string lblPSNAddTitle
		{
			get
			{
				return Resources.ResourceManager.GetString("lblPSNAddTitle", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x000AFD50 File Offset: 0x000ADF50
		internal static string lblRenameProfile
		{
			get
			{
				return Resources.ResourceManager.GetString("lblRenameProfile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x000AFD80 File Offset: 0x000ADF80
		internal static string lblResignSuccess
		{
			get
			{
				return Resources.ResourceManager.GetString("lblResignSuccess", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x000AFDB0 File Offset: 0x000ADFB0
		internal static string lblRestoring
		{
			get
			{
				return Resources.ResourceManager.GetString("lblRestoring", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x000AFDE0 File Offset: 0x000ADFE0
		internal static string lblRSSSection
		{
			get
			{
				return Resources.ResourceManager.GetString("lblRSSSection", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000AFE10 File Offset: 0x000AE010
		internal static string lblSearch
		{
			get
			{
				return Resources.ResourceManager.GetString("lblSearch", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x000AFE40 File Offset: 0x000AE040
		internal static string lblSelectCheatsFolder
		{
			get
			{
				return Resources.ResourceManager.GetString("lblSelectCheatsFolder", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x000AFE70 File Offset: 0x000AE070
		internal static string lblSelectDrive
		{
			get
			{
				return Resources.ResourceManager.GetString("lblSelectDrive", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x000AFEA0 File Offset: 0x000AE0A0
		internal static string lblSelectFolder
		{
			get
			{
				return Resources.ResourceManager.GetString("lblSelectFolder", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x000AFED0 File Offset: 0x000AE0D0
		internal static string lblSerialWait
		{
			get
			{
				return Resources.ResourceManager.GetString("lblSerialWait", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x000AFF00 File Offset: 0x000AE100
		internal static string lblUnregistered
		{
			get
			{
				return Resources.ResourceManager.GetString("lblUnregistered", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x000AFF30 File Offset: 0x000AE130
		internal static string lblUpgrade
		{
			get
			{
				return Resources.ResourceManager.GetString("lblUpgrade", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x000AFF60 File Offset: 0x000AE160
		internal static string lblUserAccount
		{
			get
			{
				return Resources.ResourceManager.GetString("lblUserAccount", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x000AFF90 File Offset: 0x000AE190
		internal static string lblUserName
		{
			get
			{
				return Resources.ResourceManager.GetString("lblUserName", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000AFFC0 File Offset: 0x000AE1C0
		internal static string lnkContactSupport
		{
			get
			{
				return Resources.ResourceManager.GetString("lnkContactSupport", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x000AFFF0 File Offset: 0x000AE1F0
		internal static Bitmap logo
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("logo", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x000B0020 File Offset: 0x000AE220
		internal static Bitmap logo_swps4us
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("logo_swps4us", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x000B0050 File Offset: 0x000AE250
		internal static Bitmap mail
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("mail", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x000B0080 File Offset: 0x000AE280
		internal static Bitmap mail_mouseover
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("mail_mouseover", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x000B00B0 File Offset: 0x000AE2B0
		internal static string mainTitle
		{
			get
			{
				return Resources.ResourceManager.GetString("mainTitle", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x000B00E0 File Offset: 0x000AE2E0
		internal static string mnuAddCheatCode
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuAddCheatCode", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x000B0110 File Offset: 0x000AE310
		internal static string mnuAdvanced
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuAdvanced", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x000B0140 File Offset: 0x000AE340
		internal static string mnuDelete
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuDelete", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x000B0170 File Offset: 0x000AE370
		internal static string mnuDeleteCheatCode
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuDeleteCheatCode", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x000B01A0 File Offset: 0x000AE3A0
		internal static string mnuDeleteSave
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuDeleteSave", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x000B01D0 File Offset: 0x000AE3D0
		internal static string mnuEditCheatCode
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuEditCheatCode", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x000B0200 File Offset: 0x000AE400
		internal static string mnuExtractProfile
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuExtractProfile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x000B0230 File Offset: 0x000AE430
		internal static string mnuRegisterPSN
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuRegisterPSN", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x000B0260 File Offset: 0x000AE460
		internal static string mnuRenameProfile
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuRenameProfile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x000B0290 File Offset: 0x000AE490
		internal static string mnuResign
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuResign", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x000B02C0 File Offset: 0x000AE4C0
		internal static string mnuRestore
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuRestore", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x000B02F0 File Offset: 0x000AE4F0
		internal static string mnuSimple
		{
			get
			{
				return Resources.ResourceManager.GetString("mnuSimple", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x000B0320 File Offset: 0x000AE520
		internal static string msgAdvModeFinish
		{
			get
			{
				return Resources.ResourceManager.GetString("msgAdvModeFinish", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x000B0350 File Offset: 0x000AE550
		internal static string msgBadBackupPath
		{
			get
			{
				return Resources.ResourceManager.GetString("msgBadBackupPath", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x000B0380 File Offset: 0x000AE580
		internal static string msgChooseCache
		{
			get
			{
				return Resources.ResourceManager.GetString("msgChooseCache", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x000B03B0 File Offset: 0x000AE5B0
		internal static string msgConfirm45
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConfirm45", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000B03E0 File Offset: 0x000AE5E0
		internal static string msgConfirmBackup
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConfirmBackup", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000B0410 File Offset: 0x000AE610
		internal static string msgConfirmCode
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConfirmCode", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x000B0440 File Offset: 0x000AE640
		internal static string msgConfirmDeactivateAccount
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConfirmDeactivateAccount", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000B0470 File Offset: 0x000AE670
		internal static string msgConfirmDelete
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConfirmDelete", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000B04A0 File Offset: 0x000AE6A0
		internal static string msgConfirmDeleteSave
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConfirmDeleteSave", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000B04D0 File Offset: 0x000AE6D0
		internal static string msgConfirmResignOverwrite
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConfirmResignOverwrite", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000B0500 File Offset: 0x000AE700
		internal static string msgConfirmRestore
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConfirmRestore", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000B0530 File Offset: 0x000AE730
		internal static string msgConnect
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConnect", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x000B0560 File Offset: 0x000AE760
		internal static string msgConnecting
		{
			get
			{
				return Resources.ResourceManager.GetString("msgConnecting", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x000B0590 File Offset: 0x000AE790
		internal static string msgDeactivate
		{
			get
			{
				return Resources.ResourceManager.GetString("msgDeactivate", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x000B05C0 File Offset: 0x000AE7C0
		internal static string msgDeactivated
		{
			get
			{
				return Resources.ResourceManager.GetString("msgDeactivated", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000B05F0 File Offset: 0x000AE7F0
		internal static string msgDownloadDec
		{
			get
			{
				return Resources.ResourceManager.GetString("msgDownloadDec", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x000B0620 File Offset: 0x000AE820
		internal static string msgDownloadEnc
		{
			get
			{
				return Resources.ResourceManager.GetString("msgDownloadEnc", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x000B0650 File Offset: 0x000AE850
		internal static string msgDownloadingList
		{
			get
			{
				return Resources.ResourceManager.GetString("msgDownloadingList", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x000B0680 File Offset: 0x000AE880
		internal static string msgDownloadPatch
		{
			get
			{
				return Resources.ResourceManager.GetString("msgDownloadPatch", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x000B06B0 File Offset: 0x000AE8B0
		internal static string msgError
		{
			get
			{
				return Resources.ResourceManager.GetString("msgError", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x000B06E0 File Offset: 0x000AE8E0
		internal static string msgInfo
		{
			get
			{
				return Resources.ResourceManager.GetString("msgInfo", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x000B0710 File Offset: 0x000AE910
		internal static string msgInvalidZip
		{
			get
			{
				return Resources.ResourceManager.GetString("msgInvalidZip", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x000B0740 File Offset: 0x000AE940
		internal static string msgMajor
		{
			get
			{
				return Resources.ResourceManager.GetString("msgMajor", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x000B0770 File Offset: 0x000AE970
		internal static string msgMaxCheats
		{
			get
			{
				return Resources.ResourceManager.GetString("msgMaxCheats", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x000B07A0 File Offset: 0x000AE9A0
		internal static string msgMinor
		{
			get
			{
				return Resources.ResourceManager.GetString("msgMinor", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000B07D0 File Offset: 0x000AE9D0
		internal static string msgNewVersion
		{
			get
			{
				return Resources.ResourceManager.GetString("msgNewVersion", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x000B0800 File Offset: 0x000AEA00
		internal static string msgNoAltProfile
		{
			get
			{
				return Resources.ResourceManager.GetString("msgNoAltProfile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000B0830 File Offset: 0x000AEA30
		internal static string msgNoCheats
		{
			get
			{
				return Resources.ResourceManager.GetString("msgNoCheats", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x000B0860 File Offset: 0x000AEA60
		internal static string msgNoProfiles
		{
			get
			{
				return Resources.ResourceManager.GetString("msgNoProfiles", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x000B0890 File Offset: 0x000AEA90
		internal static string msgNoPSNFolder
		{
			get
			{
				return Resources.ResourceManager.GetString("msgNoPSNFolder", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x000B08C0 File Offset: 0x000AEAC0
		internal static string msgNoSaves
		{
			get
			{
				return Resources.ResourceManager.GetString("msgNoSaves", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x000B08F0 File Offset: 0x000AEAF0
		internal static string msgNoupdate
		{
			get
			{
				return Resources.ResourceManager.GetString("msgNoupdate", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x000B0920 File Offset: 0x000AEB20
		internal static string msgNoValidSavesInZip
		{
			get
			{
				return Resources.ResourceManager.GetString("msgNoValidSavesInZip", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000B0950 File Offset: 0x000AEB50
		internal static string msgPatchCompleted
		{
			get
			{
				return Resources.ResourceManager.GetString("msgPatchCompleted", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x000B0980 File Offset: 0x000AEB80
		internal static string msgQuickModeFinish
		{
			get
			{
				return Resources.ResourceManager.GetString("msgQuickModeFinish", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x000B09B0 File Offset: 0x000AEBB0
		internal static string msgRestored
		{
			get
			{
				return Resources.ResourceManager.GetString("msgRestored", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x000B09E0 File Offset: 0x000AEBE0
		internal static string msgSelectCheat
		{
			get
			{
				return Resources.ResourceManager.GetString("msgSelectCheat", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x000B0A10 File Offset: 0x000AEC10
		internal static string msgUnknownSysVer
		{
			get
			{
				return Resources.ResourceManager.GetString("msgUnknownSysVer", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x000B0A40 File Offset: 0x000AEC40
		internal static string msgUnsupported
		{
			get
			{
				return Resources.ResourceManager.GetString("msgUnsupported", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x000B0A70 File Offset: 0x000AEC70
		internal static string msgUploadDec
		{
			get
			{
				return Resources.ResourceManager.GetString("msgUploadDec", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x000B0AA0 File Offset: 0x000AECA0
		internal static string msgUploadEnc
		{
			get
			{
				return Resources.ResourceManager.GetString("msgUploadEnc", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x000B0AD0 File Offset: 0x000AECD0
		internal static string msgUploadPatch
		{
			get
			{
				return Resources.ResourceManager.GetString("msgUploadPatch", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x000B0B00 File Offset: 0x000AED00
		internal static string msgWait
		{
			get
			{
				return Resources.ResourceManager.GetString("msgWait", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x000B0B30 File Offset: 0x000AED30
		internal static string msgWaitSerial
		{
			get
			{
				return Resources.ResourceManager.GetString("msgWaitSerial", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x000B0B60 File Offset: 0x000AED60
		internal static string msgWrongPath
		{
			get
			{
				return Resources.ResourceManager.GetString("msgWrongPath", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x000B0B90 File Offset: 0x000AED90
		internal static string notSelected
		{
			get
			{
				return Resources.ResourceManager.GetString("notSelected", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x000B0BC0 File Offset: 0x000AEDC0
		internal static string OldMonoMsg
		{
			get
			{
				return Resources.ResourceManager.GetString("OldMonoMsg", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x000B0BF0 File Offset: 0x000AEDF0
		internal static Icon ps3se
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("ps3se", Resources.resourceCulture);
				return (Icon)@object;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x000B0C20 File Offset: 0x000AEE20
		internal static Bitmap ps3se1
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("ps3se1", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x000B0C50 File Offset: 0x000AEE50
		internal static string rssTitle
		{
			get
			{
				return Resources.ResourceManager.GetString("rssTitle", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x000B0C80 File Offset: 0x000AEE80
		internal static Bitmap sel_drive
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("sel_drive", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x000B0CB0 File Offset: 0x000AEEB0
		internal static string String1
		{
			get
			{
				return Resources.ResourceManager.GetString("String1", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x000B0CE0 File Offset: 0x000AEEE0
		internal static string support
		{
			get
			{
				return Resources.ResourceManager.GetString("support", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x000B0D10 File Offset: 0x000AEF10
		internal static string title
		{
			get
			{
				return Resources.ResourceManager.GetString("title", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x000B0D40 File Offset: 0x000AEF40
		internal static string titleAdvDownloader
		{
			get
			{
				return Resources.ResourceManager.GetString("titleAdvDownloader", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x000B0D70 File Offset: 0x000AEF70
		internal static string titleAdvEdit
		{
			get
			{
				return Resources.ResourceManager.GetString("titleAdvEdit", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x000B0DA0 File Offset: 0x000AEFA0
		internal static string titleCancelAccount
		{
			get
			{
				return Resources.ResourceManager.GetString("titleCancelAccount", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x000B0DD0 File Offset: 0x000AEFD0
		internal static string titleChooseBackup
		{
			get
			{
				return Resources.ResourceManager.GetString("titleChooseBackup", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x000B0E00 File Offset: 0x000AF000
		internal static string titleChooseProfile
		{
			get
			{
				return Resources.ResourceManager.GetString("titleChooseProfile", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x000B0E30 File Offset: 0x000AF030
		internal static string titleCodeEntry
		{
			get
			{
				return Resources.ResourceManager.GetString("titleCodeEntry", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x000B0E60 File Offset: 0x000AF060
		internal static string titleDiffResults
		{
			get
			{
				return Resources.ResourceManager.GetString("titleDiffResults", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x000B0E90 File Offset: 0x000AF090
		internal static string titleEditCheat
		{
			get
			{
				return Resources.ResourceManager.GetString("titleEditCheat", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x000B0EC0 File Offset: 0x000AF0C0
		internal static string titleGoto
		{
			get
			{
				return Resources.ResourceManager.GetString("titleGoto", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x000B0EF0 File Offset: 0x000AF0F0
		internal static string titleImport
		{
			get
			{
				return Resources.ResourceManager.GetString("titleImport", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x000B0F20 File Offset: 0x000AF120
		internal static string titleManageProfiles
		{
			get
			{
				return Resources.ResourceManager.GetString("titleManageProfiles", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x000B0F50 File Offset: 0x000AF150
		internal static string titlePSNAdd
		{
			get
			{
				return Resources.ResourceManager.GetString("titlePSNAdd", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x000B0F80 File Offset: 0x000AF180
		internal static string titleResign
		{
			get
			{
				return Resources.ResourceManager.GetString("titleResign", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x000B0FB0 File Offset: 0x000AF1B0
		internal static string titleResignInfo
		{
			get
			{
				return Resources.ResourceManager.GetString("titleResignInfo", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x000B0FE0 File Offset: 0x000AF1E0
		internal static string titleResignMessage
		{
			get
			{
				return Resources.ResourceManager.GetString("titleResignMessage", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x000B1010 File Offset: 0x000AF210
		internal static string titleSerialEntry
		{
			get
			{
				return Resources.ResourceManager.GetString("titleSerialEntry", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x000B1040 File Offset: 0x000AF240
		internal static string titleSimpleEdit
		{
			get
			{
				return Resources.ResourceManager.GetString("titleSimpleEdit", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x000B1070 File Offset: 0x000AF270
		internal static string titleSimpleEditUploader
		{
			get
			{
				return Resources.ResourceManager.GetString("titleSimpleEditUploader", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x000B10A0 File Offset: 0x000AF2A0
		internal static string titleUpgrade
		{
			get
			{
				return Resources.ResourceManager.GetString("titleUpgrade", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x000B10D0 File Offset: 0x000AF2D0
		internal static string titleUpgrader
		{
			get
			{
				return Resources.ResourceManager.GetString("titleUpgrader", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x000B1100 File Offset: 0x000AF300
		internal static string tooltipV1
		{
			get
			{
				return Resources.ResourceManager.GetString("tooltipV1", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x000B1130 File Offset: 0x000AF330
		internal static string tooltipV2
		{
			get
			{
				return Resources.ResourceManager.GetString("tooltipV2", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x000B1160 File Offset: 0x000AF360
		internal static string tooltipV3
		{
			get
			{
				return Resources.ResourceManager.GetString("tooltipV3", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x000B1190 File Offset: 0x000AF390
		internal static string tooltipV4
		{
			get
			{
				return Resources.ResourceManager.GetString("tooltipV4", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x000B11C0 File Offset: 0x000AF3C0
		internal static string tooltipV5
		{
			get
			{
				return Resources.ResourceManager.GetString("tooltipV5", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x000B11F0 File Offset: 0x000AF3F0
		internal static string tootlTipSupported
		{
			get
			{
				return Resources.ResourceManager.GetString("tootlTipSupported", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x000B1220 File Offset: 0x000AF420
		internal static Bitmap uncheck
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("uncheck", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x000B1250 File Offset: 0x000AF450
		internal static string warnDeleteCache
		{
			get
			{
				return Resources.ResourceManager.GetString("warnDeleteCache", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x000B1280 File Offset: 0x000AF480
		internal static string warnOverwrite
		{
			get
			{
				return Resources.ResourceManager.GetString("warnOverwrite", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000B12B0 File Offset: 0x000AF4B0
		internal static string warnOverwriteAdv
		{
			get
			{
				return Resources.ResourceManager.GetString("warnOverwriteAdv", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x000B12E0 File Offset: 0x000AF4E0
		internal static string warnRestore
		{
			get
			{
				return Resources.ResourceManager.GetString("warnRestore", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x000B1310 File Offset: 0x000AF510
		internal static string warnTitle
		{
			get
			{
				return Resources.ResourceManager.GetString("warnTitle", Resources.resourceCulture) + "\0";
			}
		}

		// Token: 0x04000D2B RID: 3371
		private static ResourceManager resourceMan;

		// Token: 0x04000D2C RID: 3372
		private static CultureInfo resourceCulture;
	}
}
