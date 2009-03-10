class CreateKeywordsAndSubjects < ActiveRecord::Migration
  def self.up
    create_table :keywords, :force => true do |t|
      t.text     :name,       :null => false
      t.timestamps
    end
    
    create_table :subjects, :force => true do |t|
      t.integer  :parent_id
      t.text     :name,        :null => false
      t.timestamps
    end
    
    add_index :keywords, [:name], :name => "keywords_name_key", :unique => true
    add_index :subjects, [:name], :name => "subjects_name_key", :unique => true
    
  end

  def self.down
    remove_index :subjects, :name => :subjects_name_key
    remove_index :keywords, :name => :keywords_name_key
    drop_table :subjects
    drop_table :keywords
  end
end
