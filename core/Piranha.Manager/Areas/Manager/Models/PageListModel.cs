﻿/*
 * Copyright (c) 2016 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * https://github.com/piranhacms/piranha.core
 * 
 */

using System.Collections.Generic;
using System.Linq;

namespace Piranha.Areas.Manager.Models
{
    public class PageListModel
    {
        public class SiteInfo 
        {
            public string Id { get; set; }
            public string Title { get; set; }
        }

        #region Properties
        /// <summary>
        /// Gets/sets the available page types.
        /// </summary>
        public IList<Piranha.Models.PageType> PageTypes { get; set; }

        /// <summary>
        /// Gets/sets the current sitemap.
        /// </summary>
        public IList<Piranha.Models.SitemapItem> Sitemap { get; set; }

        /// <summary>
        /// Gets/sets the available sites.
        /// </summary>
        /// <returns></returns>
        public IList<SiteInfo> Sites { get; set; }

        /// <summary>
        /// Gets/sets the current site id.
        /// </summary>
        public string SiteId { get; set; }

        /// <summary>
        /// Gets/sets the current site title.
        /// </summary>
        public string SiteTitle { get; set; }
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageListModel() {
            PageTypes = new List<Piranha.Models.PageType>();
            Sitemap = new List<Piranha.Models.SitemapItem>();
            Sites = new List<SiteInfo>();
        }

        /// <summary>
        /// Gets the page list view model.
        /// </summary>
        /// <param name="api">The current api</param>
        /// <param name="siteId">The optional site id</param>
        /// <returns>The model</returns>
        public static PageListModel Get(Api api, string siteId) {
            var model = new PageListModel();

            var site = !string.IsNullOrEmpty(siteId) ?
                api.Sites.GetById(siteId) : api.Sites.GetDefault();
            var defaultSite = api.Sites.GetDefault();

            if (site == null)
                site = defaultSite;

            model.SiteId = site.Id == defaultSite.Id ? "" : site.Id;
            model.SiteTitle = site.Title;
            model.PageTypes = api.PageTypes.GetAll().ToList();
            model.Sitemap = api.Sites.GetSitemap(site.Id, onlyPublished: false);
            model.Sites = api.Sites.GetAll().Select(s => new SiteInfo() {
                Id = s.Id == defaultSite.Id ? "" : s.Id,
                Title = s.Title
            }).ToList();

            return model;
        }
    }
}
