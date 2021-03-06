using System.Drawing;
using System.Windows.Forms;

namespace CIT.Client.Docking
{
	internal abstract class DockOutlineBase
	{
		private Rectangle m_oldFloatWindowBounds;

		private Control m_oldDockTo;

		private DockStyle m_oldDock;

		private int m_oldContentIndex;

		private Rectangle m_floatWindowBounds;

		private Control m_dockTo;

		private DockStyle m_dock;

		private int m_contentIndex;

		private bool m_flagTestDrop = false;

		protected Rectangle OldFloatWindowBounds => m_oldFloatWindowBounds;

		protected Control OldDockTo => m_oldDockTo;

		protected DockStyle OldDock => m_oldDock;

		protected int OldContentIndex => m_oldContentIndex;

		protected bool SameAsOldValue => FloatWindowBounds == OldFloatWindowBounds && DockTo == OldDockTo && Dock == OldDock && ContentIndex == OldContentIndex;

		public Rectangle FloatWindowBounds => m_floatWindowBounds;

		public Control DockTo => m_dockTo;

		public DockStyle Dock => m_dock;

		public int ContentIndex => m_contentIndex;

		public bool FlagFullEdge => m_contentIndex != 0;

		public bool FlagTestDrop
		{
			get
			{
				return m_flagTestDrop;
			}
			set
			{
				m_flagTestDrop = value;
			}
		}

		public DockOutlineBase()
		{
			Init();
		}

		private void Init()
		{
			SetValues(Rectangle.Empty, null, DockStyle.None, -1);
			SaveOldValues();
		}

		private void SaveOldValues()
		{
			m_oldDockTo = m_dockTo;
			m_oldDock = m_dock;
			m_oldContentIndex = m_contentIndex;
			m_oldFloatWindowBounds = m_floatWindowBounds;
		}

		protected abstract void OnShow();

		protected abstract void OnClose();

		private void SetValues(Rectangle floatWindowBounds, Control dockTo, DockStyle dock, int contentIndex)
		{
			m_floatWindowBounds = floatWindowBounds;
			m_dockTo = dockTo;
			m_dock = dock;
			m_contentIndex = contentIndex;
			FlagTestDrop = true;
		}

		private void TestChange()
		{
			if (m_floatWindowBounds != m_oldFloatWindowBounds || m_dockTo != m_oldDockTo || m_dock != m_oldDock || m_contentIndex != m_oldContentIndex)
			{
				OnShow();
			}
		}

		public void Show()
		{
			SaveOldValues();
			SetValues(Rectangle.Empty, null, DockStyle.None, -1);
			TestChange();
		}

		public void Show(DockPane pane, DockStyle dock)
		{
			SaveOldValues();
			SetValues(Rectangle.Empty, pane, dock, -1);
			TestChange();
		}

		public void Show(DockPane pane, int contentIndex)
		{
			SaveOldValues();
			SetValues(Rectangle.Empty, pane, DockStyle.Fill, contentIndex);
			TestChange();
		}

		public void Show(DockPanel dockPanel, DockStyle dock, bool fullPanelEdge)
		{
			SaveOldValues();
			SetValues(Rectangle.Empty, dockPanel, dock, fullPanelEdge ? (-1) : 0);
			TestChange();
		}

		public void Show(Rectangle floatWindowBounds)
		{
			SaveOldValues();
			SetValues(floatWindowBounds, null, DockStyle.None, -1);
			TestChange();
		}

		public void Close()
		{
			OnClose();
		}
	}
}
