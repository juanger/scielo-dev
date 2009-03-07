class CreateInstitutions < ActiveRecord::Migration
  def self.up
    create_table :institutions, :force => true do |t|
      t.text     :name,       :null => false
      t.text     :url
      t.text     :abbrev
      t.integer  :parent_id
      t.text     :address
      t.integer  :country_id, :null => false
      t.text     :state
      t.text     :city
      t.text     :zipcode
      t.text     :phone
      t.text     :fax
      t.text     :other
      t.timestamps
    end

    add_index :institutions, [:name, :country_id], :name => "institutions_name_key", :unique => true

  end

  def self.down
    remove_index :institutions, :name => :institutions_name_key
    drop_table :institutions
  end
end
