class CreateCollections < ActiveRecord::Migration
  def self.up
    create_table :collections, :force => true do |t|
      t.text     :title,           :null => false
      t.integer  :country_id,      :null => false
      t.text     :state
      t.text     :city
      t.integer  :publisher_id,    :null => false
      t.text     :url
      t.text     :email
      t.text     :other
      t.boolean  :incomplete,   :default => false
      t.timestamps
    end
    
  end

  def self.down
    drop_table :collections
  end
end
