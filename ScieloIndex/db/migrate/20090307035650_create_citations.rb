class CreateCitations < ActiveRecord::Migration
  def self.up
    create_table :citations, :force => true do |t|
      t.integer  :article_id,           :null => false
      t.integer  :cited_by_article_id,  :null => false
      t.integer  :cite_order,           :null => false
      t.timestamps
    end
    
    add_index :citations, [:article_id, :cited_by_article_id], :name => "citations_article_id_key", :unique => true
    
  end

  def self.down
    remove_index :citations, :name => :citations_article_id_key
    drop_table :citations
  end
end
