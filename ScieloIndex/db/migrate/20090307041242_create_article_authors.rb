class CreateArticleAuthors < ActiveRecord::Migration
  def self.up
    create_table :article_authors, :force => true do |t|
      t.integer  :article_id,    :null => false
      t.integer  :author_id,     :null => false
      t.integer  :author_order,  :null => false
      t.timestamps
    end
    
    add_index :article_authors, [:article_id, :author_id], :name => "article_authors_article_id_key", :unique => true
    
  end

  def self.down
    remove_index :article_authors, :name => :article_authors_article_id_key
    drop_table :article_authors
  end
end
