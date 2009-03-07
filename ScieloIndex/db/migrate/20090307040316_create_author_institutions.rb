class CreateAuthorInstitutions < ActiveRecord::Migration
  def self.up
    create_table :author_institutions, :force => true do |t|
      t.integer  :author_id,       :null => false
      t.integer  :institution_id,  :null => false
      t.timestamps
    end
    
    add_index :author_institutions, [:author_id, :institution_id], :name => "author_institutions_author_id_key", :unique => true
  end

  def self.down
    remove_index :author_institutions, :name => :author_institutions_author_id_key
    drop_table :author_institutions
  end
end
