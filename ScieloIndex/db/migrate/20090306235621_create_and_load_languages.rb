require 'active_record/fixtures'

class CreateAndLoadLanguages < ActiveRecord::Migration
  def self.up
    create_table :languages, :force => true do |t|
      t.text     :name,              :null => false
      t.string     :code, :limit => 3, :null => false
      t.timestamps
    end
    
    add_index :languages, [:code], :name => "languages_code_key", :unique => true
    
    directory = File.join(File.dirname(__FILE__),"catalogs")
    Fixtures.create_fixtures(directory, "countries")
  end

  def self.down
    remove_index :languages, :name => :languages_code_key
    drop_table :languages
  end
end
