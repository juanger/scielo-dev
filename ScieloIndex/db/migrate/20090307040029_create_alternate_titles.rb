class CreateAlternateTitles < ActiveRecord::Migration
  def self.up
    create_table :alternate_titles, :force => true do |t|
      t.text    :title,        :null => false
      t.integer :language_id,  :null => false
      t.integer :article_id,   :null => false
      t.timestamps
    end
    
    add_index :alternate_titles, [:title, :language_id, :article_id], :name => "alternate_titles_title_key", :unique => true
  end

  def self.down
    remove_index :alternate_titles, :name => :alternate_titles_title_key
    drop_table :alternate_titles
  end
end
