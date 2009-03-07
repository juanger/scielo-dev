class CreateArticles < ActiveRecord::Migration
  def self.up
    create_table :articles, :force => true do |t|
      t.text     :title,             :null => false
      t.text     :subtitle
      t.integer  :language_id,       :null => false
      t.integer  :journal_issue_id,  :null => false
      t.text     :fpage
      t.text     :lpage
      t.text     :page_range
      t.text     :url
      t.text     :pacsnum
      t.text     :other
      t.timestamps
    end
    
    add_index :articles, [:title, :subtitle, :journal_issue_id], :name => "articles_title_key", :unique => true
    
  end

  def self.down
    remove_index :articles, :name => :articles_title_key
    drop_table :articles
  end
end
