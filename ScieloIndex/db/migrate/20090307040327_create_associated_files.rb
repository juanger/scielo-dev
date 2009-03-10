class CreateAssociatedFiles < ActiveRecord::Migration
  def self.up    
    create_table :associated_files, :force => true do |t|
      t.integer  :article_id,  :null => false
      t.text     :filename,    :null => false
      t.text     :pdf_path,    :null => false
      t.text     :html_path,   :null => false
      t.timestamps
    end
    
    add_index :associated_files, [:article_id], :name => "associated_files_article_id_key", :unique => true
    
  end

  def self.down
    remove_index :associated_files, :name => :associated_files_article_id_key
    drop_table :associated_files
  end
end
