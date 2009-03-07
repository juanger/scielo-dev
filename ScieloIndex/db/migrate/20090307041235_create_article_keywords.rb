class CreateArticleKeywords < ActiveRecord::Migration
  def self.up
    create_table :article_keywords, :force => true do |t|
      t.integer  :article_id,  :null => false
      t.integer  :keyword_id,  :null => false
      t.timestamps
    end
    
    add_index :article_keywords, [:article_id, :keyword_id], :name => "article_keywords_article_id_key", :unique => true
    
  end

  def self.down
    remove_index :article_keywords, :name => :article_keywords_article_id_key
    drop_table :article_keywords
  end
end
