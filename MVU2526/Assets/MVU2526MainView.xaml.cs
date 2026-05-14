#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using NoesisGUIExtensions;
using UnityEngine;
using UnityEngine.UI;
#else
using System;
using System.Windows.Controls;
#endif

namespace MVU2526
{
    /// <summary>
    /// Interaction logic for MVU2526MainView.xaml
    /// </summary>
    public partial class MVU2526MainView : UserControl
    {
        public MVU2526MainView()
        {
            InitializeComponent();
        }

#if NOESIS
    TextBlock text;
        private void InitializeComponent()
        {
            NoesisUnity.LoadComponent(this);
        }

        
        


#endif
    }
}
