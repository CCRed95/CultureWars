using System;
using System.Collections.Generic;
using System.Text;

namespace CultureWars.Data.Export.WordPress.Data.Export.WordPress
{
	public class WordPressPost
	{
		public int PostID { get; }

		public string SummaryTitle { get; }

		public string Author { get; }

		public List<string> Category { get; }

		public string Description { get; }

		public DateTime PostDateTime { get; }

		public string PostName { get; }

		public bool IsStickied { get; }

		


		
	}
}
/*
        <category><![CDATA[Punctuation]]></category>

        <category domain="category" nicename="punctuation"><![CDATA[Punctuation]]></category>

        <category domain="tag"><![CDATA[punctuation]]></category>

        <category domain="tag" nicename="punctuation-2"><![CDATA[punctuation]]></category>

        <category domain="tag"><![CDATA[question-mark]]></category>

        <category domain="tag" nicename="question-mark"><![CDATA[question-mark]]></category>

        <guid isPermaLink="false">http://mrhebrew.com/?p=136</guid>
        <description></description>
        <content:encoded><![CDATA[
text of blog here 
]]></content:encoded>
        <excerpt:encoded><![CDATA[]]></excerpt:encoded>
        <wp:post_id>136</wp:post_id>
        <wp:post_date>2011-01-05 05:52:49</wp:post_date>
        <wp:post_date_gmt>2011-01-05 05:52:49</wp:post_date_gmt>
        <wp:comment_status>open</wp:comment_status>
        <wp:ping_status>open</wp:ping_status>
        <wp:post_name>another-possible-punctuation-changes-meaning-entirely</wp:post_name>
        <wp:status>publish</wp:status>
        <wp:post_parent>0</wp:post_parent>
        <wp:menu_order>0</wp:menu_order>
        <wp:post_type>post</wp:post_type>
        <wp:post_password></wp:post_password>
        <wp:is_sticky>0</wp:is_sticky>
                                <wp:postmeta>
        <wp:meta_key>_edit_lock</wp:meta_key>
        <wp:meta_value><![CDATA[1294206847]]></wp:meta_value>
        </wp:postmeta>
                <wp:postmeta>
        <wp:meta_key>_edit_last</wp:meta_key>
        <wp:meta_value><![CDATA[1]]></wp:meta_value>
        </wp:postmeta>
                <wp:postmeta>
        <wp:meta_key>_encloseme</wp:meta_key>
        <wp:meta_value><![CDATA[1]]></wp:meta_value>
        </wp:postmeta>
                <wp:postmeta>
        <wp:meta_key>_wp_old_slug</wp:meta_key>
        <wp:meta_value><![CDATA[]]></wp:meta_value>
        </wp:postmeta>
                <wp:postmeta>
        <wp:meta_key>_pingme</wp:meta_key>
        <wp:meta_value><![CDATA[1]]></wp:meta_value>
        </wp:postmeta>
                <wp:postmeta>
        <wp:meta_key>_wp_old_slug</wp:meta_key>
        <wp:meta_value><![CDATA[another-possible-punctuation-changs-meaning-entirely]]></wp:meta_value>
        </wp:postmeta>
            </item>
    </channel>
</rss>*/
