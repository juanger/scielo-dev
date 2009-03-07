require 'active_record/fixtures'

class CreateAndLoadCountries < ActiveRecord::Migration
  def self.up    
    create_table :countries, :force => true do |t| 
      t.text      :name,     :null => false
      t.string    :code,     :null => false, :limit => 3
      t.timestamps
    end 
    
    add_index :countries, [:code], :name => "countries_code_key", :unique => true
    add_index :countries, [:name], :name => "countries_name_key", :unique => true
    
    directory = File.join(File.dirname(__FILE__),"catalogs")
    Fixtures.create_fixtures(directory, "countries")
  end

  def self.down
    remove_index :countries, :name => :countries_code_key
    remove_index :countries, :name => :countries_name_key
    
    drop_table :countries
  end
end
