class CreateAuthors < ActiveRecord::Migration
  def self.up
    create_table :authors, :force => true do |t|
      t.text     :prefix
      t.text     :firstname,   :null => false
      t.text     :middlename
      t.text     :lastname,    :null => false
      t.text     :suffix
      t.text     :degree
      t.timestamps
    end
    
    add_index :authors, [:firstname, :middlename, :lastname, :suffix], :name => "authors_firstname_key", :unique => true
    
  end

  def self.down
    remove_index :authors, :name => :authors_firstname_key
    drop_table :authors
  end
end
