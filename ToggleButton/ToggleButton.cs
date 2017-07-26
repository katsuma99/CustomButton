using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToggleButton
{
    public partial class ToggleButton : PictureBox
    {
        public enum OperationState
        {
            None,
            Select,
            Pushed
        }
        public OperationState operationState = OperationState.None;

        public bool isToggleState = false;

        private Image selectOffImage;
        public Image SelectOffImage
        {
            // Sets the method for retrieving the value of your property.
            get
            {
                return selectOffImage;
            }
            // Sets the method for setting the value of your property.
            set
            {
                selectOffImage = value;
            }
        }

        private Image selectOnImage;
        public Image SelectOnImage
        {
            // Sets the method for retrieving the value of your property.
            get
            {
                return selectOnImage;
            }
            // Sets the method for setting the value of your property.
            set
            {
                selectOnImage = value;
            }
        }

        private Image normalOffImage;
        // Declares the property.
        public Image NormalOffImage
        {
            // Sets the method for retrieving the value of your property.
            get
            {
                return normalOffImage;
            }
            // Sets the method for setting the value of your property.
            set
            {
                normalOffImage = value;
                base.Image = value;
                base.Size = value.Size;
            }
        }

        private Image normalOnImage;
        // Declares the property.
        public Image NormalOnImage
        {
            // Sets the method for retrieving the value of your property.
            get
            {
                return normalOnImage;
            }
            // Sets the method for setting the value of your property.
            set
            {
                normalOnImage = value;
            }
        }

        private Image pushedOffImage;
        // Declares the property.
        public Image PushedOffImage
        {
            // Sets the method for retrieving the value of your property.
            get
            {
                return pushedOffImage;
            }
            // Sets the method for setting the value of your property.
            set
            {
                pushedOffImage = value;
            }
        }

        private Image pushedOnImage;
        // Declares the property.
        public Image PushedOnImage
        {
            // Sets the method for retrieving the value of your property.
            get
            {
                return pushedOnImage;
            }
            // Sets the method for setting the value of your property.
            set
            {
                pushedOnImage = value;
            }
        }

        public ToggleButton()
        {
            InitializeComponent();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            operationState = OperationState.Select;
            if (isToggleState)
                Image = selectOnImage;
            else
                Image = selectOffImage;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            operationState = OperationState.None;
            if (isToggleState)
                Image = normalOnImage;
            else
                Image = normalOffImage;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            operationState = OperationState.Pushed;
            if (isToggleState)
                Image = pushedOnImage;
            else
                Image = pushedOffImage;

            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            operationState = OperationState.Select;
            isToggleState ^= true;
            if (isToggleState)
                Image = selectOnImage;
            else
                Image = selectOffImage;
            base.OnMouseUp(mevent);
        }
    }
}
