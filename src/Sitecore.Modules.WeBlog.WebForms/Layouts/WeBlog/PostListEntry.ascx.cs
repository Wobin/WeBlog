﻿using System;
using System.Drawing;
using Sitecore.Modules.WeBlog.Data.Items;
using Sitecore.Modules.WeBlog.Pipelines.GetSummary;
using Sitecore.Pipelines;

namespace Sitecore.Modules.WeBlog.WebForms.Layouts
{
    public partial class BlogPostListEntry : BaseEntrySublayout
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Size maxEntryImage = CurrentBlog.MaximumThumbnailImageSizeDimension;
            if (maxEntryImage != Size.Empty)
            {
                EntryImage.MaxWidth = maxEntryImage.Width;
                EntryImage.MaxHeight = maxEntryImage.Height;
            }

            if (EntryImage.Item != null && string.IsNullOrEmpty(EntryImage.Item[EntryImage.Field]))
                EntryImage.Field = "Image";
        }

        protected string GetSummary(EntryItem entry)
        {
            var args = new GetSummaryArgs();
            args.Entry = entry;

            CorePipeline.Run("weblogGetSummary", args, true);

            return args.Summary;
        }

        protected bool ShowComments(EntryItem entryItem)
        {
            return CurrentBlog.EnableComments.Checked && !entryItem.DisableComments.Checked;
        }
    }
}