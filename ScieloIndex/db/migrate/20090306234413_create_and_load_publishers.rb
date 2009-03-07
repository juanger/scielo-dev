require 'active_record/fixtures'

class CreateAndLoadPublishers < ActiveRecord::Migration
  def self.up
    create_table :publishers, :force => true do |t|
      t.text     :name,       :null => false
      t.text     :descr
      t.text     :url
      t.timestamps
    end

    add_index :publishers, [:name], :name => "publishers_name_key", :unique => true
    
    directory = File.join(File.dirname(__FILE__),"catalogs")
    Fixtures.create_fixtures(directory, "publishers")
  end

  def self.down
    remove_index :publishers, :name => :publishers_name_key
    
    drop_table :publishers
  end
end
