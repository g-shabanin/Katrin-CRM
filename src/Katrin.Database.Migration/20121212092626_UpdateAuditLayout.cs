using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Katrin.Database.Migrations
{
    [Migration(20121212092626)]
    public class _20121212092626_UpdateAuditLayout : Migration
    {
        public override void Up()
        {
            string layoutXml = "<XtraSerializer version=\"1.0\" application=\"View\">    <property name=\"#LayoutVersion\" />    <property name=\"GroupPanelText\" />    <property name=\"NewItemRowText\" />    <property name=\"GroupFooterShowMode\">VisibleIfExpanded</property>    <property name=\"LevelIndent\">-1</property>    <property name=\"PreviewIndent\">-1</property>    <property name=\"PreviewLineCount\">-1</property>    <property name=\"BestFitMaxRowCount\">-1</property>    <property name=\"ScrollStyle\">LiveVertScroll, LiveHorzScroll</property>    <property name=\"FocusRectStyle\">RowFocus</property>    <property name=\"HorzScrollStep\">3</property>    <property name=\"PreviewFieldName\" />    <property name=\"ViewCaptionHeight\">-1</property>    <property name=\"ShowButtonMode\">ShowForFocusedCell</property>    <property name=\"Columns\" iskey=\"true\" value=\"6\">      <property name=\"Item1\" isnull=\"true\" iskey=\"true\">        <property name=\"ColumnEditName\" />        <property name=\"Name\">colAction</property>        <property name=\"FieldName\">Action</property>      </property>      <property name=\"Item2\" isnull=\"true\" iskey=\"true\">        <property name=\"Visible\">true</property>        <property name=\"VisibleIndex\">0</property>        <property name=\"Width\">301</property>        <property name=\"ColumnEditName\" />        <property name=\"Name\">colFieldName</property>        <property name=\"OptionsColumn\" isnull=\"true\" iskey=\"true\">          <property name=\"FixedWidth\">true</property>        </property>        <property name=\"FieldName\">FieldName</property>      </property>      <property name=\"Item3\" isnull=\"true\" iskey=\"true\">        <property name=\"Visible\">true</property>        <property name=\"VisibleIndex\">2</property>        <property name=\"Width\">176</property>        <property name=\"ColumnEditName\" />        <property name=\"Name\">colNewValue</property>        <property name=\"OptionsColumn\" isnull=\"true\" iskey=\"true\">          <property name=\"FixedWidth\">true</property>        </property>        <property name=\"FieldName\">NewValue</property>      </property>      <property name=\"Item4\" isnull=\"true\" iskey=\"true\">        <property name=\"Visible\">true</property>        <property name=\"VisibleIndex\">1</property>        <property name=\"Width\">203</property>        <property name=\"ColumnEditName\" />        <property name=\"Name\">colOriginalValue</property>        <property name=\"FieldName\">OriginalValue</property>      </property>      <property name=\"Item5\" isnull=\"true\" iskey=\"true\">        <property name=\"GroupFormat\" isnull=\"true\" iskey=\"true\">          <property name=\"FormatString\">{0:yyyy/M/d (dddd) HH:mm}</property>          <property name=\"FormatType\">DateTime</property>        </property>        <property name=\"DisplayFormat\" isnull=\"true\" iskey=\"true\">          <property name=\"FormatString\">{0:yyyy/M/d (dddd) HH:mm}</property>          <property name=\"FormatType\">DateTime</property>        </property>        <property name=\"GroupInterval\">DisplayText</property>        <property name=\"Visible\">true</property>        <property name=\"VisibleIndex\">3</property>        <property name=\"Width\">142</property>        <property name=\"ColumnEditName\" />        <property name=\"Name\">colModifiedOn</property>        <property name=\"OptionsColumn\" isnull=\"true\" iskey=\"true\">          <property name=\"FixedWidth\">true</property>        </property>        <property name=\"FieldName\">CreatedOn</property>      </property>      <property name=\"Item6\" isnull=\"true\" iskey=\"true\">        <property name=\"Visible\">true</property>        <property name=\"VisibleIndex\">3</property>        <property name=\"Width\">197</property>        <property name=\"ColumnEditName\" />        <property name=\"Name\">colUserId</property>        <property name=\"FieldName\">UserId</property>      </property>    </property>    <property name=\"ViewCaption\" />    <property name=\"BorderStyle\">Default</property>    <property name=\"SynchronizeClones\">true</property>    <property name=\"DetailTabHeaderLocation\">Top</property>    <property name=\"Name\">gridView1</property>    <property name=\"DetailHeight\">350</property>    <property name=\"ActiveFilterEnabled\">true</property>    <property name=\"VertScrollTipFieldName\" />    <property name=\"GroupFormat\">{0}: [#image]{1} {2}</property>    <property name=\"OptionsView\" isnull=\"true\" iskey=\"true\">      <property name=\"ShowFilterPanelMode\">Never</property>      <property name=\"ShowIndicator\">false</property>      <property name=\"ShowGroupPanel\">false</property>      <property name=\"WaitAnimationOptions\">Panel</property>      <property name=\"RowAutoHeight\">true</property>      <property name=\"ColumnAutoWidth\">false</property>    </property>    <property name=\"OptionsBehavior\" isnull=\"true\" iskey=\"true\">      <property name=\"Editable\">false</property>      <property name=\"ReadOnly\">true</property>      <property name=\"AutoExpandAllGroups\">true</property>    </property>    <property name=\"FixedLineWidth\">2</property>    <property name=\"ChildGridLevelName\" />    <property name=\"IndicatorWidth\">-1</property>    <property name=\"OptionsSelection\" isnull=\"true\" iskey=\"true\">      <property name=\"EnableAppearanceFocusedCell\">false</property>    </property>    <property name=\"ColumnPanelRowHeight\">-1</property>    <property name=\"VertScrollVisibility\">Auto</property>    <property name=\"GroupRowHeight\">-1</property>    <property name=\"HorzScrollVisibility\">Auto</property>    <property name=\"FooterPanelHeight\">-1</property>    <property name=\"RowSeparatorHeight\">0</property>    <property name=\"RowHeight\">-1</property>    <property name=\"ActiveFilterString\" />    <property name=\"GroupSummary\" iskey=\"true\" value=\"0\" />    <property name=\"SortInfo\">~Xtra#Base64AAEAAAD/////AQAAAAAAAAAMAgAAAF1EZXZFeHByZXNzLlh0cmFHcmlkLnYxMi4xLCBWZXJzaW9uPTEyLjEuNy4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI4OGQxNzU0ZDcwMGU0OWEFAQAAADhEZXZFeHByZXNzLlh0cmFHcmlkLkNvbHVtbnMuR3JpZENvbHVtblNvcnRJbmZvQ29sbGVjdGlvbgQAAAAKZ3JvdXBDb3VudAVjbG9uZQ9jbG9uZUdyb3VwQ291bnQTQ29sbGVjdGlvbkJhc2UrbGlzdAADAAMIsgFTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYy5MaXN0YDFbW0RldkV4cHJlc3MuWHRyYUdyaWQuQ29sdW1ucy5HcmlkQ29sdW1uU29ydEluZm8sIERldkV4cHJlc3MuWHRyYUdyaWQudjEyLjEsIFZlcnNpb249MTIuMS43LjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49Yjg4ZDE3NTRkNzAwZTQ5YV1dCBxTeXN0ZW0uQ29sbGVjdGlvbnMuQXJyYXlMaXN0AgAAAAEAAAAJAwAAAAAAAAAJBAAAAAQDAAAAsgFTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYy5MaXN0YDFbW0RldkV4cHJlc3MuWHRyYUdyaWQuQ29sdW1ucy5HcmlkQ29sdW1uU29ydEluZm8sIERldkV4cHJlc3MuWHRyYUdyaWQudjEyLjEsIFZlcnNpb249MTIuMS43LjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49Yjg4ZDE3NTRkNzAwZTQ5YV1dAwAAAAZfaXRlbXMFX3NpemUIX3ZlcnNpb24EAAAwRGV2RXhwcmVzcy5YdHJhR3JpZC5Db2x1bW5zLkdyaWRDb2x1bW5Tb3J0SW5mb1tdAgAAAAgICQUAAAAAAAAAAAAAAAQEAAAAHFN5c3RlbS5Db2xsZWN0aW9ucy5BcnJheUxpc3QDAAAABl9pdGVtcwVfc2l6ZQhfdmVyc2lvbgUAAAgICQYAAAABAAAABAAAAAcFAAAAAAEAAAAAAAAABC5EZXZFeHByZXNzLlh0cmFHcmlkLkNvbHVtbnMuR3JpZENvbHVtblNvcnRJbmZvAgAAABAGAAAABAAAAAkHAAAADQMMCAAAABlEZXZFeHByZXNzLlh0cmFHcmlkLnYxMi4xBQcAAAAuRGV2RXhwcmVzcy5YdHJhR3JpZC5Db2x1bW5zLkdyaWRDb2x1bW5Tb3J0SW5mbwIAAAAJU29ydE9yZGVyCkNvbHVtbk5hbWUAAQgIAAAAAQAAAAYJAAAADWNvbE1vZGlmaWVkT24L</property>    <property name=\"FormatConditions\" iskey=\"true\" value=\"0\" />    <property name=\"GroupSummarySortInfoState\" />    <property name=\"FindFilterText\" />    <property name=\"FindPanelVisible\">false</property>  </XtraSerializer>";
            Update.Table("SavedQuery").Set(new { LayoutXml = layoutXml, ReturnedTypeId = "4AA7B81C-B70D-4BCB-95D6-2AB3AF0CBBB7", Name = "Default Audit List" })
                .Where(new { SavedQueryId = "6D4E8F82-C34D-4507-BDD9-1142DD38FD6D" });
        }
        public override void Down()
        {
            
        }
    }
}